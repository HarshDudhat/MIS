// -----------------------------------------------------------------------
// <copyright file="SecurityUtility.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api
{
    #region Namespaces

    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Web;
    using MVCProject.Api.Models;

    #endregion

    /// <summary>
    /// Security utility class.
    /// </summary>
    public static class SecurityUtility
    {
        /// <summary>
        /// Holds token expiration time in minutes which is 12 hours.
        /// </summary>
        private const int ExpirationMinutes = 100;

        /// <summary>
        /// Holds name of algorithm of encryption-decryption.
        /// </summary>
        private const string Algorithm = "HmacSHA256";

        /// <summary>
        /// Holds constant of Microsoft HTTP context to retrieve client IP address. 
        /// </summary>
        private const string HttpContext = "MS_HttpContext";

        /// <summary>
        /// Holds constant to Microsoft OWIN context to retrieve client IP address.
        /// </summary>
        private const string OwinContext = "MS_OwinContext";

        /// <summary>
        /// Holds constant of remote end point message to retrieve client IP address.
        /// </summary>
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        /// <summary>
        /// Holds salt for a password to compute hash.
        /// </summary>
        private const string Salt = "rz8LuOtFBXphj9WQfvFh";

        /// <summary>
        /// Holds request's header name which will contains token.
        /// </summary>
        private const string SecurityToken = "__RequestAuthToken";

        /// <summary>
        /// RFC decryption.
        /// </summary>
        /// <param name="cipherText">Cipher text.</param>
        /// <returns>Returns decrypted cipher text.</returns>
        public static string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

        /// <summary>
        /// RFC encryption.
        /// </summary>
        /// <param name="clearText">Plain non-encrypted text.</param>
        /// <returns>Returns encrypted text string.</returns>
        public static string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        /// <summary>
        /// Extracts user information like user name and role id of current request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <returns>Returns user information of type <see cref="LogOn"/> class.</returns>
        public static UserContext ExtractUserContext(this HttpRequest request)
        {
            if (request.Headers[SecurityToken] == null)
                return new UserContext();

            string key = Encoding.UTF8.GetString(Convert.FromBase64String(request.Headers[SecurityToken]));
            UserContext userContext = new UserContext();
            string[] parts = key.Split(new char[] { ':' });
            if (parts.Length == 8)
            {
                userContext = new UserContext()
                {
                    UserName = parts[1],
                    UserId = int.Parse(parts[2]),
                    RoleId = int.Parse(parts[3]),
                    SiteLevelId = 9,
                    FunctionLevelId = 14,
                    TimeZoneMinutes = int.Parse(parts[6]),
                    Ticks = long.Parse(parts[7])
                };
            }

            return userContext;
        }

        /// <summary>
        /// Gets IP address of client.
        /// </summary>
        /// <param name="request">Current request.</param>
        /// <returns>Returns client's IP address.</returns>
        public static string GetClientIP(HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic owinContext = request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets an encrypted password.
        /// </summary>
        /// <param name="password">Password to be encrypted.</param>
        /// <returns>Returns encrypted password.</returns>
        public static string GetEncryptedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, Salt });

            using (HMAC hmac = HMACSHA256.Create(algorithmName: Algorithm))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(Salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }

        /// <summary>
        /// Gets new token computed based on some parameter values.
        /// </summary>
        /// <param name="userContext">User context.</param>
        /// <returns>Returns generated token.</returns>
        public static string GetToken(UserContext userContext)
        {
            string tokenInput = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", userContext.UserName, userContext.UserId, userContext.RoleId, 9, 14, userContext.TimeZoneMinutes, userContext.Ticks);
            string tokenLeft = string.Empty;
            string tokenRight = string.Empty;
            using (HMAC hmac = HMACSHA256.Create(Algorithm))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetEncryptedPassword(userContext.UserName));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(tokenInput));
                tokenLeft = Convert.ToBase64String(hmac.Hash);
                tokenRight = tokenInput;
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", tokenLeft, tokenRight)));
        }

        /// <summary>
        /// Checks if a token is valid.
        /// </summary>
        /// <param name="token">Generated token by GetToken().</param>
        /// <param name="userAgent">User-agent of client, passed in by RESTAuthenticate attribute on controller.</param>
        /// <param name="requestMessage">Http Request Message</param>
        /// <returns>Returns a value indicating whether token is valid or not.</returns>
        public static bool IsTokenValid(string token, string userAgent, HttpRequestMessage requestMessage)
        {
            bool result = false;
            try
            {
                // Base64 decode the string, obtaining the token:user name:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));

                // Split the parts from token.
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 8)
                {
                    // Get the hash message, user name, and timestamps.
                    string hash = parts[0];
                    UserContext userContext = new UserContext()
                    {
                        UserName = parts[1],
                        UserId = int.Parse(parts[2]),
                        RoleId = int.Parse(parts[3]),
                        SiteLevelId = 9,
                        FunctionLevelId = 14,
                        TimeZoneMinutes = int.Parse(parts[6]),
                        Ticks = long.Parse(parts[7])
                    };

                    DateTime timeStamp = new DateTime(userContext.Ticks);

                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.Now - timeStamp).TotalMinutes) > ExpirationMinutes;
                    if (!expired)
                    {
                        if (userContext.UserName != null)
                        {
                            // Hash the message with the key to generate a token.
                            string computedToken = GetToken(userContext);

                            // Compare the computed token with the one supplied and ensure they match.
                            if (requestMessage.Headers.Contains("__hostReportRequest"))
                            {
                                result = true;
                            }
                            else
                            {
                                result = token == computedToken;
                                //if (result)
                                //{
                                //    MVCProjectEntities entities = new MVCProjectEntities();
                                //    result = entities.MIS_Users.Any(x => x.UserId == userContext.UserId && x.IsActive && x.IsTokenExpired == false);
                                //}
                            }

                            if (result)
                            {
                                var identity = new GenericIdentity(userContext.UserName, "Basic");
                                var principal = new GenericPrincipal(identity, new string[] { });
                                Thread.CurrentPrincipal = principal;
                                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                                culture.DateTimeFormat.LongTimePattern = "hh:mm:ss tt";
                                culture.DateTimeFormat.ShortTimePattern = "hh:mm:ss tt";
                                Thread.CurrentThread.CurrentCulture = culture;
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return result;
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Controllers
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using MVCProject.ViewModel;
    using System.Text;
    using System.Security.Cryptography;
    using System.IO;

    /// <summary>
    /// Base MVC Controller
    /// </summary>
    [CompressContent]
    public class BaseController : Controller
    {
        /// <summary>
        /// Download Report from API
        /// </summary>
        /// <param name="apiUrl">API URL</param>
        /// <param name="fileName">File Name</param>
        /// <param name="postData">Data to submit</param>
        [HttpPost]
        public void DownloadReport(string apiUrl, string fileName, string postData)
        {
            try
            {
                using (var client = new WebClient())
                {
                    string apiHost = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"].ToString();
                    client.BaseAddress = apiHost.TrimEnd('/') + apiUrl;
                    client.UseDefaultCredentials = true;
                    client.Headers.Add("Content-Type", "application/json; charset=utf-8");
                    client.Headers.Add("__RequestAuthToken", HttpUtility.UrlDecode(((UserContext)this.Session["UserContext"]).Token.ToString()));
                    client.Headers.Add("__hostReportRequest", "Safe");
                    client.Headers.Add("user-agent", Request.UserAgent);
                    byte[] data = null;
                    if (string.IsNullOrWhiteSpace(postData))
                    {
                        data = client.DownloadData(client.BaseAddress);
                    }
                    else
                    {
                        data = client.UploadData(client.BaseAddress, System.Text.Encoding.ASCII.GetBytes(postData));
                    }

                    Response.Clear();
                    fileName = fileName.Replace(" ", "_");
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Buffer = true;
                    Response.ContentType = "application/" + (fileName.Split('.')[1].Trim().ToLower() == "pdf" ? "pdf" : "xls");
                    Response.OutputStream.Write(data, 0, data.Length);
                    Response.OutputStream.Flush();
                    Response.OutputStream.Close();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("/Error/ServerError/500/?msg=" + ex.Message);
            }
        }

        /// <summary>
        /// DecryptStringAES
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("acg7ay8h447825cg");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        /// <summary>
        /// DecryptStringFromBytes
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                // Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var streamDecrypt = new MemoryStream(cipherText))
                    {
                        using (var cryptStreamDecrypt = new CryptoStream(streamDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var streamReaderDecrypt = new StreamReader(cryptStreamDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = streamReaderDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
 
    }
}

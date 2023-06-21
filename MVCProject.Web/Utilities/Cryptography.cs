// -----------------------------------------------------------------------
// <copyright file="Cryptography.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 
namespace MVCProject.Utilities
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Cryptography encryption/decryption methods
    /// use to secure URL parameters in System
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// Get URL query string parameters as Name Value Collection
        /// </summary>
        /// <param name="q">query string</param>
        /// <returns>Returns object of type <see cref="NameValueCollection"/> class.</returns>
        public static NameValueCollection GetParams(string q)
        {
            if (q != null && q != string.Empty)
            {
                string urlParamsValue = DecryptString(q);
                return HttpUtility.ParseQueryString(urlParamsValue);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Decrypt String
        /// </summary>
        /// <param name="cipherText">cipher Text</param>
        /// <returns>Decrypted String</returns>
        public static string DecryptString(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("acg7ay8h447825cg");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        /// <summary>
        /// Decrypt String From Bytes
        /// </summary>
        /// <param name="cipherText">cipher Text</param>
        /// <param name="key">cipher key</param>
        /// <param name="iv">cipher IV</param>
        /// <returns>Decrypted string</returns>
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

                // Create a decryptor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var decryptMemoryStream = new MemoryStream(cipherText))
                    {
                        using (var decryptCryptoStream = new CryptoStream(decryptMemoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var decryptStreamReader = new StreamReader(decryptCryptoStream))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = decryptStreamReader.ReadToEnd();
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

        /// <summary>
        /// Encrypt String To Bytes
        /// </summary>
        /// <param name="plainText">plain Text</param>
        /// <param name="key">cipher key</param>
        /// <param name="iv">cipher IV</param>
        /// <returns>Encrypted string</returns>
        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            byte[] encrypted;

            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.  
                using (var encryptMemoryStream = new MemoryStream())
                {
                    using (var encryptCryptoStream = new CryptoStream(encryptMemoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var encryptStreamWriter = new StreamWriter(encryptCryptoStream))
                        {
                            // Write all data to the stream.  
                            encryptStreamWriter.Write(plainText);
                        }

                        encrypted = encryptMemoryStream.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.  
            return encrypted;
        }
    }
}
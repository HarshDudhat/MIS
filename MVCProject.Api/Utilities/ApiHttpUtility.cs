// -----------------------------------------------------------------------
// <copyright file="ApiHttpUtility.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Utilities
{
    #region Namespaces

    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using MVCProject.Api.Models;

    #endregion

    /// <summary>
    /// API HTTP utility class.
    /// </summary>
    public static class ApiHttpUtility
    {
        /// <summary>
        /// Build response which needs to be return.
        /// </summary>
        /// <param name="request">HTTP request message.</param>
        /// <param name="isAuthenticated">Indicates whether request is authenticated or not.</param>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="messageType">Type of message.</param>
        /// <param name="message">Message to be sent.</param>
        /// <returns>Returns HTTP response message of type <see cref="HttpResponseMessage"/> class.</returns>
        public static Task<HttpResponseMessage> FromResult(HttpRequestMessage request, bool isAuthenticated, HttpStatusCode statusCode, MessageTypes messageType, string message)
        {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(request.CreateResponse(statusCode, CreateResponse(isAuthenticated, messageType, message)));
            return source.Task;
        }

        /// <summary>
        /// Creates response for request.
        /// </summary>
        /// <param name="isAuthenticated">A value indicating whether request is authenticated or not.</param>
        /// /// <param name="messageType">Type of message i.e. Info or warning.</param>
        /// <param name="message">Customized message.</param>
        /// <param name="responseToReturn">Response's result.</param>
        /// <param name="total">Total record count.</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        public static ApiResponse CreateResponse(bool isAuthenticated, MessageTypes messageType, string message = "", object responseToReturn = null, int total = 0)
        {
            ApiResponse response = new ApiResponse();
            response.IsAuthenticated = isAuthenticated;
            response.Result = responseToReturn ?? string.Empty;
            response.Message = message ?? string.Empty;
            response.MessageType = (int)messageType;
            response.Total = total == 0 ? null : total.ToString();
            return response;
        }

        /// <summary>
        /// Sending email with three compulsory parameter such as emailIdTo, subject and body except other parameters are optional. 
        /// </summary>
        /// <param name="emailIdTO">emailIdTO is email TO and send email to multiple or single email id.</param>
        /// <param name="subject">subject is email subject.</param>
        /// <param name="body">body is email body content.</param>
        /// <param name="emailIdFrom">emailIdFrom is email From.</param>
        /// <param name="emailIdCC">emailIdCC is CC of email and send email to multiple or single email id.</param>
        /// <param name="emailIdBC">emailIdBC is BC of email and send email to multiple or single email id.</param>
        /// <param name="attachmentFile">attachmentFile is attachment of email.</param>
        /// <returns>Return true or false sent mail or not, respectively.</returns>
        public static bool SendMail(string[] emailIdTO, string subject, string body, string emailIdFrom = null, string[] emailIdCC = null, string[] emailIdBC = null, string attachmentFile = null)
        {
            MailAddressCollection mailAddressCollection = new MailAddressCollection();
            emailIdFrom = emailIdFrom ?? ConfigurationManager.AppSettings["defaultSenderNotificationEmail"];

            foreach (string item in emailIdTO)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mailAddressCollection.Add(item.Trim());
                }
            }

            SmtpClient smtpClient = new SmtpClient();
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.Subject = subject;
                mailMessage.From = new MailAddress(emailIdFrom);
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.GetEncoding("utf-8");
                foreach (string item in emailIdCC ?? new string[0])
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mailMessage.CC.Add(item);
                    }
                }

                foreach (string item in emailIdBC ?? new string[0])
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        mailMessage.Bcc.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(attachmentFile))
                {
                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(attachmentFile));
                }

                foreach (MailAddress item in mailAddressCollection)
                {
                    mailMessage.To.Add(item);
                }

                smtpClient.Send(mailMessage);
                mailMessage.Attachments.ToList().ForEach(a => a.Dispose());
            }

            return true;
        }
    }
}


namespace MVCProject.Api.Utilities.Email
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Net.Mail;
    using System.Configuration;

    public class EmailUtility
    {
        private string connectionString { get; set; }
        private string fromEmail { get; set; }
        private string fromName { get; set; }
        private string smtpServer { get; set; }
        private string EnableSSl { get; set; }
        private string smtpServerPort { get; set; }
        private System.Net.Mail.MailPriority mailPriority { get; set; }
        private NetworkCredential smtpCredentials { get; set; }

        public EmailUtility(Dictionary<int, string> emailConfiguration)
        {
            this.fromEmail = emailConfiguration[(int)EmailConfiguration.SMTP_Server_Network_Credential_User_Name];
            this.fromName = emailConfiguration[(int)EmailConfiguration.From_Email_Name];
            this.mailPriority = MailPriority.Normal;
            string strUserName = emailConfiguration[(int)EmailConfiguration.SMTP_Server_Network_Credential_User_Name];
            string strPasswd = emailConfiguration[(int)EmailConfiguration.SMTP_Server_Network_Credential_Password];
            this.smtpCredentials = new NetworkCredential(strUserName, strPasswd);
            this.smtpServer = emailConfiguration[(int)EmailConfiguration.SMTP_Server_Host];
            this.EnableSSl = emailConfiguration[(int)EmailConfiguration.SMTP_Enable_SSL];
            this.smtpServerPort = emailConfiguration[(int)EmailConfiguration.SMTP_Server_Port];
        }

        //public static void SendEmail(System.Net.Mail.MailMessage m, int fiEmail_trans_id, string fstConnectionString)
        //{
        //    EmailUtility email = new EmailUtility();
        //    email.SendEmail(m, false, fiEmail_trans_id, fstConnectionString); //by deafult set it false
        //}

        public void SendEmail(System.Net.Mail.MailMessage mailMessage, Boolean async, int emailTransactionId, string connectionString)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(this.smtpServer);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = Convert.ToBoolean(EnableSSl);// This True is only if SMTP is Gmail
                int port;
                if (int.TryParse(this.smtpServerPort, out port) && port > 0)
                    smtpClient.Port = port;
                smtpClient.Host = smtpServer;
                smtpClient.Credentials = this.smtpCredentials;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailMessage.From = new MailAddress(this.fromEmail, this.fromName);

                if (async)
                {
                    SendEmailDelegate sendEmailDelegate = new SendEmailDelegate(smtpClient.Send);
                    AsyncCallback asyncCallback = new AsyncCallback(SendEmailResponse);
                    sendEmailDelegate.BeginInvoke(mailMessage, asyncCallback, sendEmailDelegate);
                }
                else
                {
                    try
                    {
                        smtpClient.Send(mailMessage);
                        EmailBL emailBL = new EmailBL();
                        emailBL.clientconnectionString = connectionString;
                        emailBL.storedProcedureName = "USP_SetEmailSentFlag";
                        emailBL.SetSentFlag(emailTransactionId);
                    }
                    catch (Exception ex)
                    {
                        AppUtility.ElmahErrorLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                AppUtility.ElmahErrorLog(ex);
            }

        }

        private delegate void SendEmailDelegate(MailMessage mailMessage);

        private static void SendEmailResponse(IAsyncResult asyncResult)
        {
            SendEmailDelegate sd = (SendEmailDelegate)(asyncResult.AsyncState);
            sd.EndInvoke(asyncResult);
        }
    }
}
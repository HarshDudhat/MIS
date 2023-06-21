
namespace MVCProject.Api.Utilities.Email
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Net.Mail;
    using System.Configuration;
    using MVCProject.Api.Models;

    public static class EmailConfig
    {
        //ASK Admin Database
        static ConnectionStringSettings adminsettings = System.Configuration.ConfigurationManager.ConnectionStrings["SAFEASKMVCEntities"];
        static string askAdminConnectionstring = adminsettings.ConnectionString;
        // for Client Database
        static string clientConnectionstring;
        public static DataTable trainingParticipantsTable;

        public static void EmailConfigCls()
        {
            //SAFEEmail.ErrorLog.CheckAndCreateLog();
            //SAFEEmail.ErrorLog.ErrorLogExe("=========================================================");
            //SAFEEmail.ErrorLog.ErrorLogExe("Scheduler Starts On " + DateTime.Now.ToString());
            //SAFEEmail.ErrorLog.ErrorLogExe("=========================================================");
            //Console.WriteLine("=========================================================");
            //Console.WriteLine("Scheduler Starts On " + DateTime.Now.ToString());
            //Console.WriteLine("=========================================================");
            start();
        }

        public static void start()
        {
            EmailBL getEmails = new EmailBL();
            if (askAdminConnectionstring != null)
            {
                getEmails.adminConnectionString = askAdminConnectionstring;
                getEmails.SetClientConnectionString();
            }

            DateTime startTime = DateTime.Now;
            //SAFEEmail.ErrorLog.ErrorLogExe("Trying connect sql and call storeprocedure........");
            if (getEmails.companyDBinfoList.Count > 0)
            {
                //reade from APp config
                String dbServer = ConfigurationManager.AppSettings["DBServer"];
                String dbUserId = ConfigurationManager.AppSettings["DBUserID"];
                String dbPassword = ConfigurationManager.AppSettings["DBPassword"];

                for (int i = 0; i < getEmails.companyDBinfoList.Count; i++)//this for loop is for Multiple Client Database Email Sending
                {

                    //SAFEEmail.ErrorLog.ErrorLogExe("Mail Sending Start for " + getEmails.motblCompanyDBinfoColl[licmpitm].CompanyName + " Company (client)");

                    // for Client Database

                    String dbName = getEmails.companyDBinfoList[i].CompanyDatabaseName;

                    //Below code is for Set New Connection String for New Client Email Sending
                    clientConnectionstring = GetConnectionString(dbServer, dbName, dbUserId, dbPassword);

                    DataTable table = null;

                    getEmails.clientconnectionString = clientConnectionstring;
                    if (getEmails.DatabaseExists())// Check that Database exsist for Client or not
                    {
                        try
                        {
                            //Make Entry in Send EMail Table for Task which DueDates are OverDue
                            getEmails.storedProcedureName = "USP_InsertDailyEmailDetail";
                            getEmails.DailyEmailEntry();
                        }
                        catch (Exception ex)
                        {
                            // SAFEEmail.ErrorLog.ErrorLogExe("Error occurred  while Entry OverDue Task Details for" + getEmails.motblCompanyDBinfoColl[licmpitm].CompanyName + " Company (client)" + " Error : " + ex.ToString());
                            AppUtility.ElmahErrorLog(ex);
                        }


                        try
                        {
                            //Get List of Email Entry to Send Respected Users
                            getEmails.storedProcedureName = "USP_GetSendingEmailsList";
                            table = getEmails.GetEmailLists();
                        }
                        catch (Exception ex2)
                        {
                            AppUtility.ElmahErrorLog(ex2);
                            //  SAFEEmail.ErrorLog.ErrorLogExe("Error occurred  while fetching Emails List for" + getEmails.motblCompanyDBinfoColl[licmpitm].CompanyName + " Company (client)" + " Error : " + ex2.ToString());
                        }

                        if (table != null)
                        {

                            foreach (DataRow dr in table.Rows)
                            {
                                // SendEmail(dr, stClientConnectionstring);
                            }
                        }
                    }
                }
            }
            DateTime endTime = DateTime.Now;
            // SAFEEmail.ErrorLog.ErrorLogExe("Execution completed on " + dtEnd.ToString());
            //  Console.WriteLine("Scheduler Ends At " + DateTime.Now.ToString());

        }

        //public static void SendEmail(USP_GetSendingEmailsList_Result emailSendDetail, string clientConnectionstring, Dictionary<int, string> emailConfig)
        //{
        //    EmailBL emailBL = new EmailBL();
        //    DataTable emailContentTable = null;

        //    emailBL.clientconnectionString = clientConnectionstring;
        //    emailBL.storedProcedureName = emailSendDetail.SPname;
        //    if (emailSendDetail.SPname == "USP_GetTaskReAssignEmailContent" || emailSendDetail.SPname == "USP_GetTaskManagementEmailContent"
        //        || emailSendDetail.SPname == "USP_GetLegalReminderEmailContent")
        //        emailContentTable = emailBL.GetEmailDetails((int)emailSendDetail.ContentId, (int)emailSendDetail.EmailTransId, 0);
        //    else if (emailSendDetail.SPname == "USP_GetIncidentFIREmailContent"
        //          || emailSendDetail.SPname == "USP_GetIncidentCompletedEmailContent"
        //        || emailSendDetail.SPname == "USP_GetIncidentReviewCompletedEmailContent")
        //        emailContentTable = emailBL.GetEmailDetails((int)emailSendDetail.ContentId, (int)emailSendDetail.SiteId, 0);
        //    else if ((emailSendDetail.SPname == "USP_GetMultiTaskMailDetail"
        //        || emailSendDetail.SPname == "USP_GetObservationMailDetail"
        //        || emailSendDetail.SPname == "USP_GetIncidentCAPAEmailContent"
        //        || emailSendDetail.SPname == "USP_GetTrainingConductEmailDetails"
        //        || emailSendDetail.SPname == "USP_GetWalkthroughCAPAEmailContent") && emailSendDetail.SendEmpID.HasValue && emailSendDetail.ContentId.HasValue)
        //        emailContentTable = emailBL.GetEmailDetails((int)emailSendDetail.ContentId, 0, (int)emailSendDetail.SendEmpID);
        //    else if (emailSendDetail.SendEmpID.HasValue && (emailSendDetail.SPname == "USP_GetEmployeeEmailDetail" || emailSendDetail.SPname == "USP_GetEmployeeNewPasswordDetails"))
        //        emailContentTable = emailBL.GetEmailDetails(0, 0, (int)emailSendDetail.SendEmpID);
        //    else if (emailSendDetail.ContentId.HasValue)
        //        emailContentTable = emailBL.GetEmailDetails((int)emailSendDetail.ContentId, 0, 0);

        //    int count;
        //    if (Convert.ToBoolean(emailSendDetail.IsSingleEmail))
        //        count = 1;
        //    else
        //        count = emailContentTable.Rows.Count;

        //    if (emailContentTable.Rows.Count == 0)
        //        count = 0;

        //    string stage = emailSendDetail.Stage == null ? string.Empty : emailSendDetail.Stage;

        //    for (int i = 0; i < count; i++)
        //    {
        //        string subject = GetEmailContent(emailContentTable, emailSendDetail.SubjectContent, i, string.Empty, stage);
        //        string body = GetEmailContent(emailContentTable, emailSendDetail.Content, i, emailSendDetail.DynamicContent == null ? string.Empty : emailSendDetail.DynamicContent, stage);
        //        MailMessage mailMessage = new MailMessage();
        //        mailMessage.IsBodyHtml = true;
        //        string[] strUserEmailIds = Convert.ToString(emailContentTable.Rows[i]["UserEmailId"]).Split(';');
        //        string[] strUserNames = Convert.ToString(emailContentTable.Rows[i]["UserName"]).Split(',');
        //        for (int j = 0; j < strUserEmailIds.Count(); j++)
        //        {
        //            try
        //            {
        //                if (strUserEmailIds[j].Trim() != "")
        //                    mailMessage.To.Add(strUserEmailIds[j].Trim());
        //            }
        //            catch (FormatException ex)
        //            {
        //                AppUtility.ElmahErrorLog(ex);
        //            }
        //        }

        //        if (emailSendDetail.EmailStageId == 6) //If incident reported then set mail priority to high
        //            mailMessage.Priority = MailPriority.High;
        //        //if (EmailUtility.BccAddress != null)
        //        //    Message.Bcc.Add(EmailUtility.BccAddress);
        //        //if (EmailUtility.Bcc1Address != null)
        //        //    Message.Bcc.Add(EmailUtility.Bcc1Address);
        //        //Message.From = EmailUtility.FromAddress;
        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        try
        //        {
        //            EmailUtility emailUtility = new EmailUtility(emailConfig);
        //            emailUtility.SendEmail(mailMessage, false, emailSendDetail.EmailTransId, clientConnectionstring); //by deafult set it false
        //            //EmailUtility.SendEmail(Message, emailSendDetail.EmailTransId, stClientConnectionstring);
        //        }
        //        catch (Exception ex)
        //        {
        //            AppUtility.ElmahErrorLog(ex);
        //            // SAFEEmail.ErrorLog.ErrorLogExe("An error occurred  while sending email.. Please check your Internet Connection");
        //        }
        //    }
        //}

        public static string GetEmailContent(DataTable valuesTable, string template, int i, string dynamicContentTemplate, string stage)
        {
            int rowCount = valuesTable != null ? valuesTable.Rows.Count : 0;
            int columnCount = valuesTable != null ? valuesTable.Columns.Count : 0;
            string columnName = string.Empty;

            if (rowCount > 0 && columnCount > 0)
            {
                if (stage != string.Empty)
                    template = template.Replace("[[Stage]]", stage);

                for (int j = 0; j < columnCount; j++)
                {
                    columnName = valuesTable.Columns[j].ColumnName;
                    if (columnName.ToLower() == "priority")
                    {
                        string strPriority = GetTaskPriority(valuesTable.Rows[i][columnName] != null ? valuesTable.Rows[i][columnName].ToString() : string.Empty);
                        template = template.Replace("[[" + columnName + "]]", strPriority);
                    }
                    else if (columnName.ToLower() == "taskstatus")
                    {
                        string strTaskStatus = GetTaskStatus(valuesTable.Rows[i][columnName] != null ? valuesTable.Rows[i][columnName].ToString() : string.Empty);
                        template = template.Replace("[[" + columnName + "]]", strTaskStatus);
                    }
                    else if (valuesTable.Columns[j].DataType == typeof(DateTime) && valuesTable.Rows[i][columnName] != (object)DBNull.Value)
                    {
                        DateTime dtValue = Convert.ToDateTime(valuesTable.Rows[i][columnName]);
                        if (dtValue.TimeOfDay == DateTime.MinValue.TimeOfDay)
                        {
                            string strdate;
                            strdate = string.Format("{0:dd-MMM-yyyy}", dtValue);
                            template = template.Replace("[[" + columnName + "]]", strdate);
                        }
                        else
                        {
                            string strdate;
                            strdate = string.Format("{0:dd-MMM-yyyy HH:mm}", dtValue);
                            template = template.Replace("[[" + columnName + "]]", strdate);
                        }
                    }
                    else
                        template = template.Replace("[[" + columnName + "]]", valuesTable.Rows[i][columnName] != null ? valuesTable.Rows[i][columnName].ToString() : string.Empty);
                    //  strTemplate = strTemplate.Replace("[[chkvaluefirComp]]", "teST");
                }

                if (dynamicContentTemplate != string.Empty)
                {
                    string dynamicContent = "";
                    string[] dynamicCount = dynamicContentTemplate.Split('@');
                    for (int count = 0; count < dynamicCount.Count(); count++)
                    {
                        for (int row = 0; row < rowCount; row++)
                        {
                            string[] contentWithTemplate = new string[rowCount];
                            string[] contentWithout = new string[rowCount];

                            contentWithTemplate[row] += dynamicCount[count];
                            contentWithout[row] = string.Empty;
                            for (int j = 0; j < columnCount; j++)
                            {
                                columnName = valuesTable.Columns[j].ColumnName;
                                if (contentWithTemplate[row].Contains("[[" + columnName + "]]"))
                                {
                                    contentWithTemplate[row] = contentWithTemplate[row].Replace("[[" + columnName + "]]", valuesTable.Rows[row][columnName] != null ? valuesTable.Rows[row][columnName].ToString() : string.Empty);
                                    contentWithout[row] += valuesTable.Rows[row][columnName] != null ? valuesTable.Rows[row][columnName].ToString() : string.Empty;
                                }
                                else
                                {
                                    contentWithTemplate[row] = contentWithTemplate[row].Replace("[[" + columnName + "]]", string.Empty);
                                    contentWithout[row] += valuesTable.Rows[row][columnName] != null ? valuesTable.Rows[row][columnName].ToString() : string.Empty;
                                }
                            }

                            if (contentWithout[row] != string.Empty)
                                dynamicContent += contentWithTemplate[row];
                        }

                        if (dynamicContent == string.Empty)
                            dynamicContent = dynamicContentTemplate;
                        template = template.Replace("[[DynamicContent" + count.ToString() + "]]", dynamicContent);
                        dynamicContent = "";
                    }
                }
            }

            return template.Replace("[NPSB]", "&nbsp;");
        }

        private static string GetTaskPriority(string priority)
        {
            switch (priority)
            {
                case "1":
                    return "Low";
                case "2":
                    return "Medium";
                case "3":
                    return "High";
                default:
                    return "";
            }
        }

        private static string GetTaskStatus(string taskStatus)
        {
            switch (taskStatus)
            {
                case "1":
                    return "Active";
                case "2":
                    return "InProgress";
                case "3":
                    return "Completed";
                case "4":
                    return "ReOpen";
                case "5":
                    return "Close";
                case "6":
                    return "Drop";
                case "7":
                    return "ReviewRequired";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Return the connection string for company
        /// </summary>
        /// <param name="hostName">IP Address or domain name</param>
        /// <param name="databaseName">Database name</param>
        /// <param name="databaseUserName">Login username for database</param>
        /// <param name="databasePassword">Login User password for database</param>
        /// <returns>return connection string with required meta tags</returns>
        public static string GetConnectionString(string hostName, string databaseName, string databaseUserName, string databasePassword)
        {
            return string.Format("data source={0};initial catalog={1};user id={2};password={3};", hostName, databaseName, databaseUserName, databasePassword);
        }

        public static bool ValidateEmailConfiguration(Dictionary<int, string> emailConfiguration)
        {
            foreach (var configuration in Enum.GetNames(typeof(EmailConfiguration)))
            {
                if (string.IsNullOrWhiteSpace(emailConfiguration[(int)Enum.Parse(typeof(EmailConfiguration), configuration)]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
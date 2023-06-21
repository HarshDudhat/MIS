// -----------------------------------------------------------------------
// <copyright file="EmailBL.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Utilities.Email
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections.ObjectModel;
    using System.Data.Common;

    public class EmailBL
    {
        public string clientconnectionString { get; set; }
        public string storedProcedureName { get; set; }
        public string adminConnectionString { get; set; }
        public class CompanyDBinfo
        {
            public string CompanyName { get; set; }
            public string CompanyDatabaseName { get; set; }
        }
        public ObservableCollection<CompanyDBinfo> companyDBinfoList;

        public DataTable GetEmailLists()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clientconnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                connection.Close();
            }
            return dataTable;
        }

        public DataTable CheckTaskDueDate()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clientconnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public void SetSentFlag(int Email_trans_id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clientconnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailTransId", Email_trans_id);
                //command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
        }

        public DataTable GetEmailDetails(int contentId, int emailTransId, int sendEmpID)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clientconnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                if (storedProcedureName == "USP_GetEmailContentTrainingPlanner")
                {
                    command.Parameters.AddWithValue("@ContentId", contentId);
                    if (emailTransId != 0)
                        command.Parameters.AddWithValue("@EmailTransId", emailTransId);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        foreach (DataTable table in dataset.Tables)
                        {
                            if (table != null)
                            {
                                if (dataTable.Rows.Count == 0)
                                {
                                    dataTable = table;
                                }
                                else
                                {
                                    // EmailConfig.DtTrainingParticipants = table;
                                }
                            }
                        }
                    }
                }
                else if (storedProcedureName == "USP_GetIncidentFIREmailContent"
                    || storedProcedureName == "USP_GetIncidentCompletedEmailContent"
                    || storedProcedureName == "USP_GetIncidentReviewCompletedEmailContent")
                {
                    command.Parameters.AddWithValue("@inContentId", contentId);
                    command.Parameters.AddWithValue("@inContractorId", emailTransId);
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dataTable);
                }
                else if (sendEmpID > 0 && (storedProcedureName == "USP_GetMultiTaskMailDetail"
                    || storedProcedureName == "USP_GetObservationMailDetail"
                    || storedProcedureName == "USP_GetIncidentCAPAEmailContent"
                    || storedProcedureName == "USP_GetTrainingConductEmailDetails"
                    || storedProcedureName == "USP_GetWalkthroughCAPAEmailContent"))
                {
                    command.Parameters.AddWithValue("@ContentId", contentId);
                    command.Parameters.AddWithValue("@SendEmpID", sendEmpID);
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dataTable);
                }
                else if (sendEmpID > 0 && storedProcedureName == "USP_GetEmployeeEmailDetail")//User Subscription alert
                {
                    command.Parameters.AddWithValue("@EmployeeId ", sendEmpID);
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dataTable);
                }
                else if (sendEmpID > 0 && storedProcedureName == "USP_GetEmployeeNewPasswordDetails")
                {
                    command.Parameters.AddWithValue("@EmployeeId ", sendEmpID);
                    string url = AppUtility.GetRequestUrl();
                    string link = url + "/Account/ResetPassword?userId=[[UserId]]&resetPasswordToken=[[ResetPasswordToken]]";
                    command.Parameters.AddWithValue("@Link ", link);
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dataTable);
                }
                else if (contentId > 0)
                {
                    command.Parameters.AddWithValue("@ContentId", contentId);
                    if (emailTransId != 0)
                        command.Parameters.AddWithValue("@EmailTransId", emailTransId);
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dataTable);
                }

            }
            return dataTable;
        }

        public void SetClientConnectionString()
        {
            using (SqlConnection adminDbConn = new SqlConnection(adminConnectionString))
            {
                adminDbConn.Open();
                SqlCommand command = new SqlCommand("USP_GetCompanyDBInfo", adminDbConn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = command.ExecuteReader();
                companyDBinfoList = new ObservableCollection<CompanyDBinfo>();
                while (sqlDataReader.Read())
                {
                    CompanyDBinfo companyDBinfo = new CompanyDBinfo();
                    companyDBinfo.CompanyName = (string)sqlDataReader["CompanyName"];
                    companyDBinfo.CompanyDatabaseName = (string)sqlDataReader["CompanyDatabaseName"];
                    companyDBinfoList.Add(companyDBinfo);
                }
                sqlDataReader.Close();
                adminDbConn.Close();
            }
        }

        public bool DatabaseExists()
        {
            DbConnection db = new SqlConnection(clientconnectionString);
            try
            {
                db.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        //Make Entry in Send EMail Table for Task which DueDates are OverDue
        public void DailyEmailEntry()
        {
            using (SqlConnection connection = new SqlConnection(clientconnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteScalar();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    AppUtility.ElmahErrorLog(ex);
                    connection.Close();
                }
            }
        }
    }
}
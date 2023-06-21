// -----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Utilities
{
    using MVCProject.Api.Models;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlDatabaseUtility
    {
        private static MVCProjectEntities entities = new MVCProjectEntities();

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get
            {
                string connectionString = ((System.Data.EntityClient.EntityConnection)entities.Connection).StoreConnection.ConnectionString;
                return connectionString;
            }
        }

        private static SqlConnection GetConnection()
        {
            string connectionString = ((System.Data.EntityClient.EntityConnection)entities.Connection).StoreConnection.ConnectionString;
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            return cn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlConnection MakeConnection()
        {
            SqlConnection newConnection = new SqlConnection(this.ConnectionString);
            if (newConnection.State == ConnectionState.Closed)
            {
                newConnection.Open();
            }
            return newConnection;
        }

        public static SqlDataReader ExecuteQuery(string storedProcName, List<SqlParameter> procParameters)
        {
            // open a database connection
            SqlConnection cn = GetConnection();

            // create a SQL command to execute the stored procedure
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcName;

            // assign parameters passed in to the command
            foreach (var procParameter in procParameters)
            {
                cmd.Parameters.Add(procParameter);
            }
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet ExecuteCommand(string command, SqlParameter parameters)
        {
            DataSet dataset = new DataSet();

            using (SqlConnection connection = this.MakeConnection())
            {
                using (SqlCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = command;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 0;
                    dbCommand.Parameters.Add(parameters);
                    dbCommand.Connection = connection;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand: dbCommand))
                    {
                        dataAdapter.Fill(dataSet: dataset);
                    }
                }
            }

            return dataset;
        }

        public DataSet ExecuteCommandArray(string command, SqlParameter[] parameters)
        {
            DataSet dataset = new DataSet();

            using (SqlConnection connection = this.MakeConnection())
            {
                using (SqlCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = command;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandTimeout = 0;
                    dbCommand.Parameters.AddRange(parameters);
                    dbCommand.Connection = connection;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand: dbCommand))
                    {
                        dataAdapter.Fill(dataSet: dataset);
                    }
                }
            }

            return dataset;
        }

        public static int ExecuteCommand(string storedProcName, Dictionary<string, SqlParameter> procParameters)
        {
            int rc;

            using (SqlConnection cn = GetConnection())
            {
                // create a SQL command to execute the stored procedure
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcName;

                // assign parameters passed in to the command
                foreach (var procParameter in procParameters)
                {
                    cmd.Parameters.Add(procParameter.Value);
                }

                rc = cmd.ExecuteNonQuery();
            }

            return rc;
        }

    }
}
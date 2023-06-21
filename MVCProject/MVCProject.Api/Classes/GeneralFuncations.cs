using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace MVCProject.Api.Utilities
{
    public class GeneralFuncations
    {
        public string Connstr = Convert.ToString(HttpContext.Current.Items["elmah-express-connection"]);// ConfigurationManager.ConnectionStrings["elmah-express"].ConnectionString;

        public DataSet GetDatatableFromSP(string Method, IDictionary<string, string> parameters, int NoOfDataTables = 0)
        {
            DataSet DS = getdatasetFromParams("USP_" + Method, parameters);
            return DS;
        }

        public DataSet getdatasetFromParams(string Squery, IDictionary<string, string> parameters)
        {
            DataSet ds = new DataSet();
            var conn = new SqlConnection(Connstr);

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = Squery;//
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var itm in parameters)
                {
                    cmd.Parameters.AddWithValue("@" + itm.Key, itm.Value);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Close();
                // this will query your database and return the result to your datatable
                da.Fill(ds);

                da.Dispose();
                conn.Close();
            }
            return ds;
        }
    }
}
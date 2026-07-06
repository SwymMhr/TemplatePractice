using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace TemplatingPractice.DAL
{
    public static class DAO
    {
        public static string connString = WebConfigurationManager.ConnectionStrings["templateConnectionString"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(connString);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }            
            return con;
        }

        public static int ExecuteQuery(string query, SqlParameter[] param)
        {
            using(SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    if (param != null)
                        cmd.Parameters.AddRange(param);
                    return cmd.ExecuteNonQuery();
                }                
            }            
        }

        public static DataTable GetTableQuery(string query, SqlParameter[] param)
        {
            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    if (param != null)
                        cmd.Parameters.AddRange(param);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
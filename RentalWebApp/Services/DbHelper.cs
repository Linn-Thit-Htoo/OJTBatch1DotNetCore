using System.Data;
using System.Data.SqlClient;

namespace RentalWebApp.Services
{
    public class DbHelper
    {
        public static DataTable Query(string query, params SqlParameter[] sqlParameters)
        {
            try
            {
                SqlConnection conn = new(GetConnectionStringService._connStr);
                conn.Open();
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddRange(sqlParameters);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int Execute(string query, params SqlParameter[] sqlParameters)
        {
            try
            {
                SqlConnection conn = new(GetConnectionStringService._connStr);
                conn.Open();
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddRange(sqlParameters);
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

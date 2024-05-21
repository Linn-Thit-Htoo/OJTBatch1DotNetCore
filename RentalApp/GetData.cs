using System.Data;
using System.Data.SqlClient;

namespace RentalApp;

public class GetData
{
    public static DataTable Fetch(string tableName)
    {
        try
        {
            SqlConnection conn = new(GetConnectionString._connStr);
            conn.Open();
            string query = $"SELECT * FROM {tableName} WHERE IsActive = @IsActive";
            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@IsActive", true);
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
}
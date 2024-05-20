using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace OJTBatch1DotNetCore.RestApiDemo.Services;

public class AdoDotNetService
{
    private readonly IConfiguration _configuration;

    public AdoDotNetService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #region Query

    public List<T> Query<T>(string query, SqlParameter[]? parameters = null)
    {
        SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
        conn.Open();
        SqlCommand cmd = new(query, conn);
        cmd.Parameters.AddRange(parameters);
        SqlDataAdapter adapter = new(cmd);
        DataTable dt = new();
        adapter.Fill(dt);
        conn.Close();

        string jsonStr = JsonConvert.SerializeObject(dt);
        List<T> lst = JsonConvert.DeserializeObject<List<T>>(jsonStr)!;

        return lst;
    }
    #endregion

    public DataTable QueryFirstOrDefault(string query, SqlParameter[]? parameters)
    {
        SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
        conn.Open();
        SqlCommand cmd = new(query, conn);
        cmd.Parameters.AddRange(parameters);
        SqlDataAdapter adapter = new(cmd);
        DataTable dt = new();
        adapter.Fill(dt);
        conn.Close();

        return dt;
    }

    public int Execute(string query, SqlParameter[]? parameters = null)
    {
        SqlConnection conn = new(_configuration.GetConnectionString("DbConnection"));
        conn.Open();
        SqlCommand cmd = new(query, conn);
        cmd.Parameters.AddRange(parameters);
        int result = cmd.ExecuteNonQuery();
        conn.Close();

        return result;
    }
}
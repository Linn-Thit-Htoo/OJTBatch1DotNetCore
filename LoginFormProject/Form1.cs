using System.Data;
using System.Data.SqlClient;

namespace LoginFormProject
{
    public partial class Form1 : Form
    {
        public string _connectionStr = "Data Source=(local);Initial Catalog=OJT-Batch1;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                if (IsNullOrEmpty(email, password))
                {
                    MessageBox.Show("Please fill all fields...", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlConnection conn = new(_connectionStr);
                conn.Open();
                string query = @"SELECT [UserId]
      ,[UserName]
      ,[Email]
      ,[IsActive]
  FROM [dbo].[Users] WHERE Email = @Email AND Password = @Password AND IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@IsActive", true);

                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Login Successful.", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Login Fail!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsNullOrEmpty(params string[] str)
        {
            return str.Any(x => string.IsNullOrEmpty(x));
        }
    }
}
using System.Data;
using System.Data.SqlClient;

namespace RentalApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string phoneNumber = txtPhoneNumber.Text;
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please fill all fields...", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlConnection conn = new(GetConnectionString._connStr);
                conn.Open();

                string query = @"SELECT [UserId]
      ,[UserName]
      ,[PhoneNumber]
      ,[Password]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE PhoneNumber = @PhoneNumber AND Password = @Password AND IsActive = @IsActive
AND UserRole = @UserRole";

                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@IsActive", true);
                cmd.Parameters.AddWithValue("@UserRole", "admin");

                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);

                conn.Close();

                if (dt.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Login Successful!", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        DashboardForm dashboardForm = new();
                        dashboardForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Login Fail!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNumber.Text = string.Empty;
                    txtPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
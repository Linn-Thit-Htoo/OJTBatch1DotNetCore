using System.Data;
using System.Data.SqlClient;

namespace RentalApp;

public partial class CreateNewUserForm : Form
{
    public CreateNewUserForm()
    {
        InitializeComponent();
    }

    private void CreateNewUserForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = txtUserName.Text;
            string phoneNumber = txtPhoneNumber.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please fill all fields...", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // duplicate
            if (IsPhoneNumberDuplicate(phoneNumber))
            {
                MessageBox.Show("User with this phone number already exists!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection conn = new(GetConnectionString._connStr);
            conn.Open();

            string query = @"INSERT INTO Users (UserName, PhoneNumber, UserRole, IsActive)
VALUES(@UserName, @PhoneNumber, @UserRole, @IsActive)";

            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@UserRole", "user");
            cmd.Parameters.AddWithValue("@IsActive", true);

            int result = cmd.ExecuteNonQuery();

            conn.Close();

            if (result > 0)
            {
                Clear();
                DialogResult dialog = MessageBox.Show("Creating Successful!", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialog == DialogResult.OK)
                {
                    GoToUserManagementForm();
                }
            }
            else
            {
                Clear();
                MessageBox.Show("Creating Fail!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool IsPhoneNumberDuplicate(string phoneNumber)
    {
        try
        {
            SqlConnection conn = new(GetConnectionString._connStr);
            conn.Open();
            string query = @"SELECT UserId, UserName, PhoneNumber, UserRole, IsActive
FROM Users WHERE PhoneNumber = @PhoneNumber AND IsActive = @IsActive";
            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@IsActive", true);
            SqlDataAdapter adapter = new(cmd);
            DataTable dt = new();
            adapter.Fill(dt);
            conn.Close();

            return dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private void Clear()
    {
        txtUserName.Text = "";
        txtPhoneNumber.Text = string.Empty;
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        GoToUserManagementForm();
    }
    private void GoToUserManagementForm()
    {
        UserManagementForm userManagementForm = new();
        userManagementForm.Show();
        this.Hide();
    }
}
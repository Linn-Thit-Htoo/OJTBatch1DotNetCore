using System.Data;
using System.Data.SqlClient;

namespace RentalApp;

public partial class EditUserForm : Form
{
    public long _id;
    public EditUserForm()
    {
        InitializeComponent();
    }

    private void EditUserForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
        GoToUserManagementForm();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string searchData = txtSearchPhoneNumber.Text;

            if (string.IsNullOrEmpty(searchData))
            {
                MessageBox.Show("Please fill search data...", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection conn = new(GetConnectionString._connStr);
            conn.Open();
            string query = @"SELECT [UserId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [dbo].[Users] WHERE PhoneNumber = @PhoneNumber AND IsActive = @IsActive";
            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@PhoneNumber", searchData);
            cmd.Parameters.AddWithValue("@IsActive", true);
            SqlDataAdapter adapter = new(cmd);
            DataTable dt = new();
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("User not found or inactive.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _id = Convert.ToInt64(dt.Rows[0]["UserId"]);
            txtUserName.Text = dt.Rows[0]["UserName"].ToString();
            txtPhoneNumber.Text = dt.Rows[0]["PhoneNumber"].ToString();
            cbo1.Text = dt.Rows[0]["UserRole"].ToString()!.ToLower();
            txtSearchPhoneNumber.Text = "";
            btnUpdate.Enabled = true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void EditUserForm_Load(object sender, EventArgs e)
    {
        btnUpdate.Enabled = false;
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = txtUserName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string userRole = cbo1.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(userRole))
            {
                MessageBox.Show("Please fill all fields...", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsPhoneNumberDuplicate(phoneNumber, _id))
            {
                MessageBox.Show("User with this phone number already exists!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = new(GetConnectionString._connStr);
            conn.Open();
            string query = @"UPDATE Users SET UserName = @UserName, PhoneNumber = @PhoneNumber, 
UserRole = @UserRole
WHERE UserId = @UserId";
            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@UserRole", userRole);
            cmd.Parameters.AddWithValue("@UserId", _id);
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            if (result > 0)
            {
                DialogResult dialog = MessageBox.Show("Updating Successful!", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialog == DialogResult.OK)
                {
                    Clear();
                    GoToUserManagementForm();
                }
            }
            else
            {
                Clear();
                MessageBox.Show("Updating Fail!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private bool IsPhoneNumberDuplicate(string phoneNumber, long id)
    {
        try
        {
            SqlConnection conn = new(GetConnectionString._connStr);
            conn.Open();

            string query = @"SELECT
       [UserId]
      ,[UserName]
      ,[PhoneNumber]
      ,[UserRole]
      ,[IsActive]
  FROM [OJT-Batch1].[dbo].[Users]
WHERE PhoneNumber = @PhoneNumber AND UserId != @UserId AND IsActive = @IsActive";

            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@UserId", id);
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
    private void GoToUserManagementForm()
    {
        UserManagementForm userManagementForm = new();
        userManagementForm.Show();
        this.Hide();
    }
    private void Clear()
    {
        txtUserName.Text = "";
        txtPhoneNumber.Text = "";
        cbo1.Text = "";
    }
}

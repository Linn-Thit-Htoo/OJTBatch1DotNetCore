using System.Data;
using System.Data.SqlClient;

namespace RentalApp
{
    public partial class CreateCategoryForm : Form
    {
        public CreateCategoryForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string categoryName = txtCategoryName.Text;

                if (string.IsNullOrEmpty(categoryName))
                {
                    MessageBox.Show("Please fill all fields...", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (IsCategoryNameDuplicate(categoryName))
                {
                    Clear();
                    MessageBox.Show("Category name already exists!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlConnection conn = new(GetConnectionString._connStr);
                conn.Open();
                string query = @"INSERT INTO Category (CategoryName, IsActive) VALUES(@CategoryName, @IsActive)";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                cmd.Parameters.AddWithValue("@IsActive", true);
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result > 0)
                {
                    Clear();
                    DialogResult dialog = MessageBox.Show("Creating Successful!", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialog == DialogResult.OK)
                    {
                        GoToCategoryManagementForm();
                    }
                }
                else
                {
                    MessageBox.Show("Creating Fail!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private bool IsCategoryNameDuplicate(string categoryName)
        {
            try
            {
                SqlConnection conn = new(GetConnectionString._connStr);
                conn.Open();
                string query = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE CategoryName = @CategoryName AND IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
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

        private void CreateCategoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GoToCategoryManagementForm();
        }
        private void GoToCategoryManagementForm()
        {
            CategoryManagementForm categoryManagementForm = new();
            categoryManagementForm.Show();
            this.Hide();
        }
        private void Clear()
        {
            txtCategoryName.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}

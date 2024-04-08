using System.Data;
using System.Data.SqlClient;

namespace RentalApp
{
    public partial class EditCategoryForm : Form
    {
        public readonly long _id;
        public readonly string _categoryName;
        public EditCategoryForm(long id, string categoryName)
        {
            InitializeComponent();
            this._id = id;
            this._categoryName = categoryName;
        }

        private void EditCategoryForm_Load(object sender, EventArgs e)
        {
            txtCategoryName.Text = _categoryName;
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

        private void EditCategoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

                if (IsCategoryNameDuplicate(_id, categoryName))
                {
                    Clear();
                    MessageBox.Show("Category name already exists!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlConnection conn = new(GetConnectionString._connStr);
                conn.Open();
                string query = @"UPDATE Category SET CategoryName = @CategoryName
WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                cmd.Parameters.AddWithValue("@CategoryId", _id);
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result > 0)
                {
                    Clear();
                    DialogResult dialog = MessageBox.Show("Updating Successful!", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialog == DialogResult.OK)
                    {
                        GoToCategoryManagementForm();
                    }
                }
                else
                {
                    MessageBox.Show("Updating Fail!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private bool IsCategoryNameDuplicate(long id, string categoryName)
        {
            try
            {
                SqlConnection conn = new(GetConnectionString._connStr);
                conn.Open();
                string query = @"SELECT [CategoryId]
      ,[CategoryName]
      ,[IsActive]
  FROM [dbo].[Category] WHERE CategoryName = @CategoryName AND IsActive = @IsActive AND CategoryId != @CategoryId";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                cmd.Parameters.AddWithValue("@IsActive", true);
                cmd.Parameters.AddWithValue("@CategoryId", id);
                SqlDataAdapter adpter = new(cmd);
                DataTable dt = new();
                adpter.Fill(dt);
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
            txtCategoryName.Text = "";
        }
    }
}

using System.Data;
using System.Data.SqlClient;

namespace RentalApp
{
    public partial class CategoryManagementForm : Form
    {
        public CategoryManagementForm()
        {
            InitializeComponent();
        }

        private void CategoryManagementForm_Load(object sender, EventArgs e)
        {
            try
            {
                FetchData();

                DataGridViewButtonColumn editBtn = new()
                {
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                };
                editBtn.DefaultCellStyle.BackColor = Color.Green;
                dgv1.Columns.Add(editBtn);

                DataGridViewButtonColumn deleteBtn = new()
                {
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                };
                deleteBtn.DefaultCellStyle.BackColor = Color.Red;
                dgv1.Columns.Add(deleteBtn);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CategoryManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateCategoryForm createCategoryForm = new();
            createCategoryForm.Show();
            this.Hide();
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            long id = Convert.ToInt64(dgv1.Rows[e.RowIndex].Cells[0].Value);
            if (e.ColumnIndex == 3)
            {
                // edit case
                string categoryName = Convert.ToString(dgv1.Rows[e.RowIndex].Cells[1].Value)!;
                EditCategoryForm editCategoryForm = new(id, categoryName);
                editCategoryForm.Show();
                this.Hide();
            }
            if (e.ColumnIndex == 4)
            {
                // delete case
                DialogResult dialog = MessageBox.Show("Are you sure you want to delete?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    try
                    {
                        SqlConnection conn = new(GetConnectionString._connStr);
                        conn.Open();
                        string query = @"UPDATE Category SET IsActive = @IsActive WHERE CategoryId = @CategoryId";
                        SqlCommand cmd = new(query, conn);
                        cmd.Parameters.AddWithValue("@IsActive", false);
                        cmd.Parameters.AddWithValue("@CategoryId", id);
                        int result = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (result > 0)
                        {
                            MessageBox.Show("Deleting Successful!", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FetchData();
                            return;
                        }
                        MessageBox.Show("Deleting Fail!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        private void FetchData()
        {
            try
            {
                DataTable dt = GetData.Fetch("Category");
                dgv1.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

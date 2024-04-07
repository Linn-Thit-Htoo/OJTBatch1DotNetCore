using System.Data;
using System.Data.SqlClient;

namespace RentalApp
{
    public partial class UserManagementForm : Form
    {
        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void UserManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            try
            {
                FetchData();

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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateNewUserForm createNewUserForm = new();
            createNewUserForm.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditUserForm editUserForm = new();
            editUserForm.Show();
            this.Hide();
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                try
                {
                    DialogResult dialog = MessageBox.Show("Are you sure you want to delete?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK)
                    {
                        long id = Convert.ToInt64(dgv1.Rows[e.RowIndex].Cells[0].Value);
                        SqlConnection conn = new(GetConnectionString._connStr);
                        conn.Open();
                        string query = @"UPDATE Users SET IsActive = @IsActive WHERE UserId = @UserId";
                        SqlCommand cmd = new(query, conn);
                        cmd.Parameters.AddWithValue("@IsActive", false);
                        cmd.Parameters.AddWithValue("@UserId", id);
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
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private void FetchData()
        {
            try
            {
                SqlConnection conn = new(GetConnectionString._connStr);
                conn.Open();
                string query = @"SELECT * FROM Users WHERE IsActive = @IsActive";
                SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IsActive", true);
                SqlDataAdapter adapter = new(cmd);
                DataTable dt = new();
                adapter.Fill(dt);
                conn.Close();

                dgv1.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

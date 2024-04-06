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
                SqlConnection conn = new(GetConnectionString._connStr);
                conn.Open();
                string query = @"SELECT * FROM Users";
                SqlCommand cmd = new(query, conn);
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateNewUserForm createNewUserForm = new();
            createNewUserForm.Show();
            this.Hide();
        }
    }
}

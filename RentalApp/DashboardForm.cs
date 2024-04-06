namespace RentalApp
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void msHome_Click(object sender, EventArgs e)
        {

        }

        private void msUserManagement_Click(object sender, EventArgs e)
        {
            UserManagementForm userManagementForm = new();
            userManagementForm.Show();
            this.Hide();
        }

        private void DashboardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

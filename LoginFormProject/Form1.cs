namespace LoginFormProject
{
    public partial class Form1 : Form
    {
        public string _email = "linnthit77387@gmail.com";
        public string _password = "123123";

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

                if (email.Equals(_email) && password.Equals(_password)) // Equals ==
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
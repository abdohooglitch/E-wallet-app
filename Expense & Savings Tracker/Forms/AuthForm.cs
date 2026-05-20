using ExpenseSavingsTracker.Database;

namespace ExpenseSavingsTracker.Forms
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string mobile = txtMobileNumber.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter mobile number and password.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatabaseHelper.GetUserByMobile(mobile) == null)
            {
                MessageBox.Show("You are not registered. Please sign up first.", "Not Registered",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = DatabaseHelper.Login(mobile, password);
            if (user == null)
            {
                MessageBox.Show("Invalid password.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var dashboard = new DashboardForm(user.Id);
            Hide();
            dashboard.ShowDialog();
            Show();
            txtMobileNumber.Clear();
            txtPassword.Clear();
        }

        private void BtnSignUp_Click(object? sender, EventArgs e)
        {
            using var signup = new SignupForm();
            signup.ShowDialog();
        }
    }
}

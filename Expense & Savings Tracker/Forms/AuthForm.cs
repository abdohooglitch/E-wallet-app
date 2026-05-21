using ExpenseSavingsTracker.Database;

namespace ExpenseSavingsTracker.Forms
{
    /// <summary>
    /// Login screen: users sign in with mobile number and password, or open registration.
    /// </summary>
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates input, checks registration, verifies password, then opens the dashboard.
        /// </summary>
        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string mobile = txtMobileNumber.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Require both fields before attempting login
            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter mobile number and password.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // User must register before they can log in
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

            // Hide login form while dashboard is open; show again after logout
            using var dashboard = new DashboardForm(user.Id);
            Hide();
            dashboard.ShowDialog();
            Show();
            txtMobileNumber.Clear();
            txtPassword.Clear();
        }

        /// <summary>Opens the sign-up form as a modal dialog.</summary>
        private void BtnSignUp_Click(object? sender, EventArgs e)
        {
            using var signup = new SignupForm();
            signup.ShowDialog();
        }
    }
}

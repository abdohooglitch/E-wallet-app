using ExpenseSavingsTracker.Database;

namespace ExpenseSavingsTracker.Forms
{
    /// <summary>
    /// Registration screen for new users (name, mobile number, password).
    /// </summary>
    public partial class SignupForm : Form
    {
        public SignupForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates fields and saves a new user to the database.
        /// </summary>
        private void BtnSignUp_Click(object? sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string mobile = txtMobileNumber.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatabaseHelper.RegisterUser(name, mobile, password))
            {
                MessageBox.Show("Account created! Please log in.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // Duplicate mobile number triggers unique constraint failure
                MessageBox.Show("Sign up failed. Mobile number may already be registered.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Closes the form without creating an account.</summary>
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

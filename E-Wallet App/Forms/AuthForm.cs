using System;
using System.Drawing;
using System.Windows.Forms;
using EWalletApp.Database;
using EWalletApp.UI;

namespace EWalletApp.Forms
{
    // Form 1: Sign Up and Login Form
    // This form handles user authentication and is the first form shown.
    public class AuthForm : Form
    {
        private TextBox txtMobileNumber;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnSignUp;
        private Label lblTitle;
        private Label lblMobile;
        private Label lblPassword;

        public AuthForm()
        {
            InitializeComponents();
        }

        // Initialize UI Components
        private void InitializeComponents()
        {
            this.Text = "E-Wallet App - Login / Sign Up";
            this.Size = new Size(420, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            UiTheme.ApplyForm(this);

            lblTitle = new Label
            {
                Text = "Welcome to E-Wallet App",
                Location = new Point(55, 35),
                AutoSize = true
            };
            UiTheme.StyleTitle(lblTitle);

            lblMobile = new Label { Text = "Mobile Number:", Location = new Point(55, 100), AutoSize = true };
            lblPassword = new Label { Text = "Password:", Location = new Point(55, 165), AutoSize = true };
            UiTheme.StyleLabel(lblMobile);
            UiTheme.StyleLabel(lblPassword);

            txtMobileNumber = new TextBox { Location = new Point(55, 122), Width = 300 };
            txtPassword = new TextBox { Location = new Point(55, 187), Width = 300, UseSystemPasswordChar = true };
            UiTheme.StyleTextBox(txtMobileNumber);
            UiTheme.StyleTextBox(txtPassword);

            btnLogin = new Button { Text = "Login", Location = new Point(55, 245), Width = 145, Height = 42 };
            btnLogin.Click += BtnLogin_Click;
            UiTheme.StylePrimaryButton(btnLogin);

            btnSignUp = new Button { Text = "Sign Up", Location = new Point(210, 245), Width = 145, Height = 42 };
            btnSignUp.Click += BtnSignUp_Click;
            UiTheme.StyleSuccessButton(btnSignUp);

            // Add controls to form
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblMobile);
            this.Controls.Add(txtMobileNumber);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnSignUp);
        }

        // Event handler for Login button
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string mobile = txtMobileNumber.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Simple validation
            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both mobile number and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if user is registered first
            var existingUser = DatabaseHelper.GetUserByMobile(mobile);
            if (existingUser == null)
            {
                MessageBox.Show("You are not registered. Please sign up.", "Not Registered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Call database helper to login
            var user = DatabaseHelper.Login(mobile, password);
            if (user != null)
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Open the Balance Form (Dashboard)
                BalanceForm balanceForm = new BalanceForm(user.Id);
                this.Hide();
                balanceForm.ShowDialog();
                this.Show(); // Show login form again when dashboard is closed
                
                // Clear fields
                txtMobileNumber.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for Sign Up button
        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm();
            signupForm.ShowDialog();
        }
    }
}

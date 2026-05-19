using System;
using System.Drawing;
using System.Windows.Forms;
using EWalletApp.Database;
using EWalletApp.UI;

namespace EWalletApp.Forms
{
    public class SignupForm : Form
    {
        private TextBox txtName;
        private TextBox txtMobileNumber;
        private TextBox txtPassword;
        private Button btnSignUp;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblName;
        private Label lblMobile;
        private Label lblPassword;

        public SignupForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "E-Wallet App - Sign Up";
            this.Size = new Size(420, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            UiTheme.ApplyForm(this);

            lblTitle = new Label
            {
                Text = "Create an Account",
                Location = new Point(95, 35),
                AutoSize = true
            };
            UiTheme.StyleTitle(lblTitle);

            lblName = new Label { Text = "Name:", Location = new Point(55, 90), AutoSize = true };
            lblMobile = new Label { Text = "Mobile Number:", Location = new Point(55, 155), AutoSize = true };
            lblPassword = new Label { Text = "Password:", Location = new Point(55, 220), AutoSize = true };
            UiTheme.StyleLabel(lblName);
            UiTheme.StyleLabel(lblMobile);
            UiTheme.StyleLabel(lblPassword);

            txtName = new TextBox { Location = new Point(55, 112), Width = 300 };
            txtMobileNumber = new TextBox { Location = new Point(55, 177), Width = 300 };
            txtPassword = new TextBox { Location = new Point(55, 242), Width = 300, UseSystemPasswordChar = true };
            UiTheme.StyleTextBox(txtName);
            UiTheme.StyleTextBox(txtMobileNumber);
            UiTheme.StyleTextBox(txtPassword);

            btnSignUp = new Button { Text = "Sign Up", Location = new Point(55, 305), Width = 145, Height = 42 };
            btnSignUp.Click += BtnSignUp_Click;
            UiTheme.StyleSuccessButton(btnSignUp);

            btnCancel = new Button { Text = "Cancel", Location = new Point(210, 305), Width = 145, Height = 42 };
            btnCancel.Click += BtnCancel_Click;
            UiTheme.StyleDangerButton(btnCancel);

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblMobile);
            this.Controls.Add(txtMobileNumber);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnSignUp);
            this.Controls.Add(btnCancel);
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string mobile = txtMobileNumber.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Name, Mobile Number, and Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = DatabaseHelper.RegisterUser(name, mobile, password);
            if (success)
            {
                MessageBox.Show("Sign Up Successful! Please log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sign Up Failed. Mobile number might already be registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

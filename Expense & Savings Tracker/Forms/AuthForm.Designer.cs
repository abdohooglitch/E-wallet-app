namespace ExpenseSavingsTracker.Forms
{
    partial class AuthForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblMobile;
        private Label lblPassword;
        private TextBox txtMobileNumber;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnSignUp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblMobile = new Label();
            lblPassword = new Label();
            txtMobileNumber = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnSignUp = new Button();
            SuspendLayout();

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Expense & Savings Tracker - Login";
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.75F);

            // lblTitle
            lblTitle.Text = "Expense & Savings Tracker";
            lblTitle.Location = new Point(50, 35);
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);

            // lblMobile
            lblMobile.Text = "Mobile Number:";
            lblMobile.Location = new Point(55, 105);
            lblMobile.AutoSize = true;
            lblMobile.ForeColor = Color.FromArgb(51, 65, 85);

            txtMobileNumber.Location = new Point(55, 128);
            txtMobileNumber.Size = new Size(360, 28);
            txtMobileNumber.BorderStyle = BorderStyle.FixedSingle;

            // lblPassword
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(55, 170);
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.FromArgb(51, 65, 85);

            txtPassword.Location = new Point(55, 193);
            txtPassword.Size = new Size(360, 28);
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            // btnLogin
            btnLogin.Text = "Login";
            btnLogin.Location = new Point(55, 265);
            btnLogin.Size = new Size(175, 44);
            StylePrimaryButton(btnLogin);
            btnLogin.Click += BtnLogin_Click;

            // btnSignUp
            btnSignUp.Text = "Sign Up";
            btnSignUp.Location = new Point(240, 265);
            btnSignUp.Size = new Size(175, 44);
            StyleSuccessButton(btnSignUp);
            btnSignUp.Click += BtnSignUp_Click;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblMobile, txtMobileNumber, lblPassword, txtPassword, btnLogin, btnSignUp
            });
            ResumeLayout(false);
            PerformLayout();
        }

        private static void StylePrimaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(37, 99, 235);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private static void StyleSuccessButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(5, 150, 105);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}

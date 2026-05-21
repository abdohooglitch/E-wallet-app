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
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(50, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(415, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Expense & Savings Tracker";
            // 
            // lblMobile
            // 
            lblMobile.AutoSize = true;
            lblMobile.ForeColor = Color.FromArgb(51, 65, 85);
            lblMobile.Location = new Point(55, 105);
            lblMobile.Name = "lblMobile";
            lblMobile.Size = new Size(155, 28);
            lblMobile.TabIndex = 1;
            lblMobile.Text = "Mobile Number:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.FromArgb(51, 65, 85);
            lblPassword.Location = new Point(55, 170);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(97, 28);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password:";
            // 
            // txtMobileNumber
            // 
            txtMobileNumber.BorderStyle = BorderStyle.FixedSingle;
            txtMobileNumber.Location = new Point(55, 128);
            txtMobileNumber.Name = "txtMobileNumber";
            txtMobileNumber.Size = new Size(360, 33);
            txtMobileNumber.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Location = new Point(55, 193);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(360, 33);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(55, 265);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(175, 44);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnSignUp
            // 
            btnSignUp.Location = new Point(240, 265);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(175, 44);
            btnSignUp.TabIndex = 6;
            btnSignUp.Text = "Sign Up";
            btnSignUp.Click += BtnSignUp_Click;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(480, 420);
            Controls.Add(lblTitle);
            Controls.Add(lblMobile);
            Controls.Add(txtMobileNumber);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnSignUp);
            Font = new Font("Segoe UI", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Expense & Savings Tracker - Login";
            Load += AuthForm_Load;
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

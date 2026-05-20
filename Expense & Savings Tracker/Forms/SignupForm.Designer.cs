namespace ExpenseSavingsTracker.Forms
{
    partial class SignupForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblName;
        private Label lblMobile;
        private Label lblPassword;
        private TextBox txtName;
        private TextBox txtMobileNumber;
        private TextBox txtPassword;
        private Button btnSignUp;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblName = new Label();
            lblMobile = new Label();
            lblPassword = new Label();
            txtName = new TextBox();
            txtMobileNumber = new TextBox();
            txtPassword = new TextBox();
            btnSignUp = new Button();
            btnCancel = new Button();
            SuspendLayout();

            ClientSize = new Size(480, 500);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create Account";
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.75F);

            lblTitle.Text = "Create Your Account";
            lblTitle.Location = new Point(95, 30);
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);

            lblName.Text = "Full Name:";
            lblName.Location = new Point(55, 88);
            lblName.AutoSize = true;
            lblName.ForeColor = Color.FromArgb(51, 65, 85);

            txtName.Location = new Point(55, 110);
            txtName.Size = new Size(360, 28);

            lblMobile.Text = "Mobile Number:";
            lblMobile.Location = new Point(55, 150);
            lblMobile.AutoSize = true;
            lblMobile.ForeColor = Color.FromArgb(51, 65, 85);

            txtMobileNumber.Location = new Point(55, 172);
            txtMobileNumber.Size = new Size(360, 28);

            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(55, 212);
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.FromArgb(51, 65, 85);

            txtPassword.Location = new Point(55, 234);
            txtPassword.Size = new Size(360, 28);
            txtPassword.UseSystemPasswordChar = true;

            btnSignUp.Text = "Sign Up";
            btnSignUp.Location = new Point(55, 310);
            btnSignUp.Size = new Size(175, 44);
            StyleSuccessButton(btnSignUp);
            btnSignUp.Click += BtnSignUp_Click;

            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(240, 310);
            btnCancel.Size = new Size(175, 44);
            StyleDangerButton(btnCancel);
            btnCancel.Click += BtnCancel_Click;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblName, txtName, lblMobile, txtMobileNumber,
                lblPassword, txtPassword, btnSignUp, btnCancel
            });
            ResumeLayout(false);
            PerformLayout();
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

        private static void StyleDangerButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(220, 38, 38);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}

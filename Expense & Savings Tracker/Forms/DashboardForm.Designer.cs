namespace ExpenseSavingsTracker.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblWelcome;
        private Panel pnlExpenseCard;
        private Panel pnlSavingsCard;
        private Label lblExpenseTitle;
        private Label lblExpenseAmount;
        private Label lblSavingsTitle;
        private Label lblSavingsAmount;
        private Panel pnlBudget;
        private Label lblBudgetTitle;
        private Label lblBudgetInfo;
        private ProgressBar progressBudget;
        private Label lblBudgetStatus;
        private TextBox txtMonthlyBudget;
        private Button btnSaveBudget;
        private Button btnAddExpense;
        private Button btnExpenseHistory;
        private Button btnSavings;
        private Button btnReports;
        private Button btnRefresh;
        private Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            pnlExpenseCard = new Panel();
            pnlSavingsCard = new Panel();
            lblExpenseTitle = new Label();
            lblExpenseAmount = new Label();
            lblSavingsTitle = new Label();
            lblSavingsAmount = new Label();
            pnlBudget = new Panel();
            lblBudgetTitle = new Label();
            lblBudgetInfo = new Label();
            progressBudget = new ProgressBar();
            lblBudgetStatus = new Label();
            txtMonthlyBudget = new TextBox();
            btnSaveBudget = new Button();
            btnAddExpense = new Button();
            btnExpenseHistory = new Button();
            btnSavings = new Button();
            btnReports = new Button();
            btnRefresh = new Button();
            btnLogout = new Button();
            pnlExpenseCard.SuspendLayout();
            pnlSavingsCard.SuspendLayout();
            pnlBudget.SuspendLayout();
            SuspendLayout();

            ClientSize = new Size(520, 600);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.75F);

            lblWelcome.Text = "Welcome, User";
            lblWelcome.Location = new Point(24, 20);
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(15, 23, 42);

            // Expense card
            pnlExpenseCard.BackColor = Color.FromArgb(254, 242, 242);
            pnlExpenseCard.Location = new Point(28, 65);
            pnlExpenseCard.Size = new Size(220, 95);
            lblExpenseTitle.Text = "This Month Expenses";
            lblExpenseTitle.Location = new Point(12, 12);
            lblExpenseTitle.AutoSize = true;
            lblExpenseTitle.ForeColor = Color.FromArgb(185, 28, 28);
            lblExpenseTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblExpenseAmount.Text = "Rs 0";
            lblExpenseAmount.Location = new Point(12, 38);
            lblExpenseAmount.AutoSize = true;
            lblExpenseAmount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblExpenseAmount.ForeColor = Color.FromArgb(220, 38, 38);
            pnlExpenseCard.Controls.Add(lblExpenseTitle);
            pnlExpenseCard.Controls.Add(lblExpenseAmount);

            // Savings card
            pnlSavingsCard.BackColor = Color.FromArgb(236, 253, 245);
            pnlSavingsCard.Location = new Point(264, 65);
            pnlSavingsCard.Size = new Size(228, 95);
            lblSavingsTitle.Text = "Total Savings";
            lblSavingsTitle.Location = new Point(12, 12);
            lblSavingsTitle.AutoSize = true;
            lblSavingsTitle.ForeColor = Color.FromArgb(4, 120, 87);
            lblSavingsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSavingsAmount.Text = "Rs 0";
            lblSavingsAmount.Location = new Point(12, 38);
            lblSavingsAmount.AutoSize = true;
            lblSavingsAmount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblSavingsAmount.ForeColor = Color.FromArgb(5, 150, 105);
            pnlSavingsCard.Controls.Add(lblSavingsTitle);
            pnlSavingsCard.Controls.Add(lblSavingsAmount);

            // Budget panel
            pnlBudget.BackColor = Color.White;
            pnlBudget.Location = new Point(28, 175);
            pnlBudget.Size = new Size(464, 140);
            lblBudgetTitle.Text = "Monthly Budget";
            lblBudgetTitle.Location = new Point(14, 12);
            lblBudgetTitle.AutoSize = true;
            lblBudgetTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBudgetTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblBudgetInfo.Text = "Set limit:";
            lblBudgetInfo.Location = new Point(14, 40);
            lblBudgetInfo.AutoSize = true;
            txtMonthlyBudget.Location = new Point(95, 38);
            txtMonthlyBudget.Size = new Size(140, 28);
            btnSaveBudget.Text = "Save Budget";
            btnSaveBudget.Location = new Point(250, 34);
            btnSaveBudget.Size = new Size(110, 34);
            StyleSecondaryButton(btnSaveBudget);
            btnSaveBudget.Click += BtnSaveBudget_Click;
            progressBudget.Location = new Point(14, 78);
            progressBudget.Size = new Size(430, 24);
            progressBudget.Style = ProgressBarStyle.Continuous;
            lblBudgetStatus.Text = "No budget set";
            lblBudgetStatus.Location = new Point(14, 108);
            lblBudgetStatus.MaximumSize = new Size(430, 0);
            lblBudgetStatus.AutoSize = true;
            lblBudgetStatus.ForeColor = Color.FromArgb(71, 85, 105);
            pnlBudget.Controls.AddRange(new Control[]
            {
                lblBudgetTitle, lblBudgetInfo, txtMonthlyBudget, btnSaveBudget, progressBudget, lblBudgetStatus
            });

            btnAddExpense.Text = "+ Add Expense";
            btnAddExpense.Location = new Point(28, 335);
            btnAddExpense.Size = new Size(225, 44);
            StyleExpenseButton(btnAddExpense);
            btnAddExpense.Click += BtnAddExpense_Click;

            btnExpenseHistory.Text = "Expense History";
            btnExpenseHistory.Location = new Point(267, 335);
            btnExpenseHistory.Size = new Size(225, 44);
            StylePrimaryButton(btnExpenseHistory);
            btnExpenseHistory.Click += BtnExpenseHistory_Click;

            btnSavings.Text = "Savings Goals";
            btnSavings.Location = new Point(28, 392);
            btnSavings.Size = new Size(225, 44);
            StyleSuccessButton(btnSavings);
            btnSavings.Click += BtnSavings_Click;

            btnReports.Text = "Monthly Report";
            btnReports.Location = new Point(267, 392);
            btnReports.Size = new Size(225, 44);
            StyleAccentButton(btnReports);
            btnReports.Click += BtnReports_Click;

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(28, 450);
            btnRefresh.Size = new Size(225, 40);
            StyleSecondaryButton(btnRefresh);
            btnRefresh.Click += BtnRefresh_Click;

            btnLogout.Text = "Logout";
            btnLogout.Location = new Point(267, 450);
            btnLogout.Size = new Size(225, 40);
            StyleDangerButton(btnLogout);
            btnLogout.Click += BtnLogout_Click;

            Controls.AddRange(new Control[]
            {
                lblWelcome, pnlExpenseCard, pnlSavingsCard, pnlBudget,
                btnAddExpense, btnExpenseHistory, btnSavings, btnReports, btnRefresh, btnLogout
            });

            pnlExpenseCard.ResumeLayout(false);
            pnlExpenseCard.PerformLayout();
            pnlSavingsCard.ResumeLayout(false);
            pnlSavingsCard.PerformLayout();
            pnlBudget.ResumeLayout(false);
            pnlBudget.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void StylePrimaryButton(Button b) => StyleButton(b, Color.FromArgb(37, 99, 235));
        private static void StyleExpenseButton(Button b) => StyleButton(b, Color.FromArgb(234, 88, 12));
        private static void StyleSuccessButton(Button b) => StyleButton(b, Color.FromArgb(5, 150, 105));
        private static void StyleAccentButton(Button b) => StyleButton(b, Color.FromArgb(124, 58, 237));
        private static void StyleSecondaryButton(Button b) => StyleButton(b, Color.FromArgb(71, 85, 105));
        private static void StyleDangerButton(Button b) => StyleButton(b, Color.FromArgb(220, 38, 38));

        private static void StyleButton(Button button, Color color)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}

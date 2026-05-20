namespace ExpenseSavingsTracker.Forms
{
    partial class SavingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private DataGridView dgvGoals;
        private GroupBox grpNewGoal;
        private TextBox txtGoalTitle;
        private TextBox txtTargetAmount;
        private Button btnCreateGoal;
        private GroupBox grpActions;
        private TextBox txtAmount;
        private Button btnDeposit;
        private Button btnWithdraw;
        private Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            dgvGoals = new DataGridView();
            grpNewGoal = new GroupBox();
            txtGoalTitle = new TextBox();
            txtTargetAmount = new TextBox();
            btnCreateGoal = new Button();
            grpActions = new GroupBox();
            txtAmount = new TextBox();
            btnDeposit = new Button();
            btnWithdraw = new Button();
            btnBack = new Button();
            Label lblGoalName = new Label();
            Label lblTarget = new Label();
            Label lblAmountLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvGoals).BeginInit();
            grpNewGoal.SuspendLayout();
            grpActions.SuspendLayout();
            SuspendLayout();

            ClientSize = new Size(620, 520);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Savings Goals";
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.75F);

            lblTitle.Text = "My Savings Goals";
            lblTitle.Location = new Point(24, 18);
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(5, 150, 105);

            dgvGoals.Location = new Point(24, 50);
            dgvGoals.Size = new Size(570, 200);
            dgvGoals.ReadOnly = true;
            dgvGoals.AllowUserToAddRows = false;
            dgvGoals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGoals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGoals.RowHeadersVisible = false;
            dgvGoals.BackgroundColor = Color.White;
            dgvGoals.EnableHeadersVisualStyles = false;
            dgvGoals.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(5, 150, 105);
            dgvGoals.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGoals.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);

            grpNewGoal.Text = "Create New Goal";
            grpNewGoal.Location = new Point(24, 262);
            grpNewGoal.Size = new Size(270, 120);
            lblGoalName.Text = "Goal name:";
            lblGoalName.Location = new Point(14, 28);
            lblGoalName.AutoSize = true;
            txtGoalTitle.Location = new Point(14, 50);
            txtGoalTitle.Size = new Size(240, 25);
            lblTarget.Text = "Target (Rs):";
            lblTarget.Location = new Point(14, 78);
            lblTarget.AutoSize = true;
            txtTargetAmount.Location = new Point(90, 75);
            txtTargetAmount.Size = new Size(100, 25);
            btnCreateGoal.Text = "Create";
            btnCreateGoal.Location = new Point(200, 73);
            btnCreateGoal.Size = new Size(54, 28);
            StyleSuccessButton(btnCreateGoal);
            btnCreateGoal.Click += BtnCreateGoal_Click;
            grpNewGoal.Controls.AddRange(new Control[] { lblGoalName, txtGoalTitle, lblTarget, txtTargetAmount, btnCreateGoal });

            grpActions.Text = "Selected Goal";
            grpActions.Location = new Point(310, 262);
            grpActions.Size = new Size(284, 120);
            lblAmountLabel.Text = "Amount (Rs):";
            lblAmountLabel.Location = new Point(14, 32);
            lblAmountLabel.AutoSize = true;
            txtAmount.Location = new Point(14, 54);
            txtAmount.Size = new Size(120, 25);
            btnDeposit.Text = "Deposit";
            btnDeposit.Location = new Point(145, 50);
            btnDeposit.Size = new Size(60, 30);
            StyleSuccessButton(btnDeposit);
            btnDeposit.Click += BtnDeposit_Click;
            btnWithdraw.Text = "Withdraw";
            btnWithdraw.Location = new Point(210, 50);
            btnWithdraw.Size = new Size(65, 30);
            StyleSecondaryButton(btnWithdraw);
            btnWithdraw.Click += BtnWithdraw_Click;
            grpActions.Controls.AddRange(new Control[] { lblAmountLabel, txtAmount, btnDeposit, btnWithdraw });

            btnBack.Text = "Back to Dashboard";
            btnBack.Location = new Point(210, 400);
            btnBack.Size = new Size(200, 40);
            StyleSecondaryButton(btnBack);
            btnBack.Click += BtnBack_Click;

            Controls.Add(lblTitle);
            Controls.Add(dgvGoals);
            Controls.Add(grpNewGoal);
            Controls.Add(grpActions);
            Controls.Add(btnBack);
            ((System.ComponentModel.ISupportInitialize)dgvGoals).EndInit();
            grpNewGoal.ResumeLayout(false);
            grpNewGoal.PerformLayout();
            grpActions.ResumeLayout(false);
            grpActions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void StyleSuccessButton(Button b) => StyleButton(b, Color.FromArgb(5, 150, 105));
        private static void StyleSecondaryButton(Button b) => StyleButton(b, Color.FromArgb(71, 85, 105));

        private static void StyleButton(Button button, Color color)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}

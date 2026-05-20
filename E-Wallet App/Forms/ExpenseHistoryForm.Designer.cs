namespace FinanceTracker.Forms
{
    partial class ExpenseHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private DataGridView dgvExpenses;
        private Label lblTotal;
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
            dgvExpenses = new DataGridView();
            lblTotal = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            SuspendLayout();

            ClientSize = new Size(640, 480);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Expense History";
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.75F);

            lblTitle.Text = "Expense History (This Month)";
            lblTitle.Location = new Point(24, 20);
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);

            dgvExpenses.Location = new Point(24, 55);
            dgvExpenses.Size = new Size(590, 320);
            dgvExpenses.ReadOnly = true;
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.AllowUserToDeleteRows = false;
            dgvExpenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.RowHeadersVisible = false;
            dgvExpenses.BackgroundColor = Color.White;
            dgvExpenses.BorderStyle = BorderStyle.None;
            dgvExpenses.EnableHeadersVisualStyles = false;
            dgvExpenses.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(234, 88, 12);
            dgvExpenses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvExpenses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dgvExpenses.ColumnHeadersHeight = 34;
            dgvExpenses.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 247, 237);

            lblTotal.Text = "Total: Rs 0";
            lblTotal.Location = new Point(24, 385);
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(220, 38, 38);

            btnBack.Text = "Back to Dashboard";
            btnBack.Location = new Point(220, 415);
            btnBack.Size = new Size(200, 40);
            StyleSecondaryButton(btnBack);
            btnBack.Click += BtnBack_Click;

            Controls.Add(lblTitle);
            Controls.Add(dgvExpenses);
            Controls.Add(lblTotal);
            Controls.Add(btnBack);
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void StyleSecondaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(71, 85, 105);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}

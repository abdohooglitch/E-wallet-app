namespace ExpenseSavingsTracker.Forms
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblMonth;
        private ComboBox cmbMonth;
        private ComboBox cmbYear;
        private Button btnLoad;
        private DataGridView dgvReport;
        private Label lblGrandTotal;
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
            lblMonth = new Label();
            cmbMonth = new ComboBox();
            cmbYear = new ComboBox();
            btnLoad = new Button();
            dgvReport = new DataGridView();
            lblGrandTotal = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            SuspendLayout();

            ClientSize = new Size(600, 520);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Monthly Report";
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.75F);

            lblTitle.Text = "Expense Report by Category";
            lblTitle.Location = new Point(24, 18);
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(124, 58, 237);

            lblMonth.Text = "Period:";
            lblMonth.Location = new Point(24, 55);
            lblMonth.AutoSize = true;

            cmbMonth.Location = new Point(75, 52);
            cmbMonth.Size = new Size(140, 28);
            cmbMonth.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbYear.Location = new Point(205, 52);
            cmbYear.Size = new Size(90, 28);
            cmbYear.DropDownStyle = ComboBoxStyle.DropDownList;

            btnLoad.Text = "Load Report";
            btnLoad.Location = new Point(310, 48);
            btnLoad.Size = new Size(130, 34);
            StyleAccentButton(btnLoad);
            btnLoad.Click += BtnLoad_Click;

            dgvReport.Location = new Point(24, 95);
            dgvReport.Size = new Size(548, 300);
            dgvReport.ReadOnly = true;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.RowHeadersVisible = false;
            dgvReport.BackgroundColor = Color.White;
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 58, 237);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);

            lblGrandTotal.Text = "Grand Total: Rs 0";
            lblGrandTotal.Location = new Point(24, 385);
            lblGrandTotal.AutoSize = true;
            lblGrandTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGrandTotal.ForeColor = Color.FromArgb(124, 58, 237);

            btnBack.Text = "Back to Dashboard";
            btnBack.Location = new Point(190, 450);
            btnBack.Size = new Size(220, 44);
            StyleSecondaryButton(btnBack);
            btnBack.Click += BtnBack_Click;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblMonth, cmbMonth, cmbYear, btnLoad, dgvReport, lblGrandTotal, btnBack
            });
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void StyleAccentButton(Button b) => StyleButton(b, Color.FromArgb(124, 58, 237));
        private static void StyleSecondaryButton(Button b) => StyleButton(b, Color.FromArgb(71, 85, 105));

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

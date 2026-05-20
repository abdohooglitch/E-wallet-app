namespace ExpenseSavingsTracker.Forms
{
    partial class ExpenseForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblAmount;
        private TextBox txtAmount;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblNote;
        private TextBox txtNote;
        private Button btnSave;
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
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblAmount = new Label();
            txtAmount = new TextBox();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblNote = new Label();
            txtNote = new TextBox();
            btnSave = new Button();
            btnBack = new Button();
            SuspendLayout();

            ClientSize = new Size(480, 460);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add Expense";
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.75F);

            lblTitle.Text = "Add New Expense";
            lblTitle.Location = new Point(55, 25);
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(234, 88, 12);

            lblCategory.Text = "Category:";
            lblCategory.Location = new Point(55, 75);
            lblCategory.AutoSize = true;
            cmbCategory.Location = new Point(55, 98);
            cmbCategory.Size = new Size(360, 28);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            lblAmount.Text = "Amount (Rs):";
            lblAmount.Location = new Point(55, 135);
            lblAmount.AutoSize = true;
            txtAmount.Location = new Point(55, 158);
            txtAmount.Size = new Size(360, 28);

            lblDate.Text = "Date:";
            lblDate.Location = new Point(55, 195);
            lblDate.AutoSize = true;
            dtpDate.Location = new Point(55, 218);
            dtpDate.Size = new Size(360, 28);
            dtpDate.Format = DateTimePickerFormat.Short;

            lblNote.Text = "Note (optional):";
            lblNote.Location = new Point(55, 255);
            lblNote.AutoSize = true;
            txtNote.Location = new Point(55, 278);
            txtNote.Size = new Size(360, 28);

            btnSave.Text = "Save Expense";
            btnSave.Location = new Point(55, 355);
            btnSave.Size = new Size(175, 44);
            StyleExpenseButton(btnSave);
            btnSave.Click += BtnSave_Click;

            btnBack.Text = "Back to Dashboard";
            btnBack.Location = new Point(240, 355);
            btnBack.Size = new Size(175, 44);
            StyleSecondaryButton(btnBack);
            btnBack.Click += BtnBack_Click;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblCategory, cmbCategory, lblAmount, txtAmount,
                lblDate, dtpDate, lblNote, txtNote, btnSave, btnBack
            });
            ResumeLayout(false);
            PerformLayout();
        }

        private static void StyleExpenseButton(Button b) => StyleButton(b, Color.FromArgb(234, 88, 12));
        private static void StyleSecondaryButton(Button b) => StyleButton(b, Color.FromArgb(71, 85, 105));

        private static void StyleButton(Button button, Color color)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}

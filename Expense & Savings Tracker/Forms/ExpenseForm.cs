using ExpenseSavingsTracker.Database;

namespace ExpenseSavingsTracker.Forms
{
    public partial class ExpenseForm : Form
    {
        private readonly int _userId;

        public ExpenseForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            cmbCategory.Items.AddRange(DatabaseHelper.ExpenseCategories);
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount greater than zero.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper.AddExpense(
                _userId,
                cmbCategory.SelectedItem.ToString()!,
                amount,
                dtpDate.Value.Date,
                txtNote.Text.Trim());

            MessageBox.Show($"Expense of Rs {amount:N0} saved.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void BtnBack_Click(object? sender, EventArgs e) => Close();
    }
}

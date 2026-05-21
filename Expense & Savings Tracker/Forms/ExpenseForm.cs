using ExpenseSavingsTracker.Database;

namespace ExpenseSavingsTracker.Forms
{
    /// <summary>
    /// Form to record a new expense: category, amount, date, and optional note.
    /// </summary>
    public partial class ExpenseForm : Form
    {
        private readonly int _userId;

        public ExpenseForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            // Populate category dropdown from shared list in DatabaseHelper
            cmbCategory.Items.AddRange(DatabaseHelper.ExpenseCategories);
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;
        }

        /// <summary>
        /// Validates category and amount, then saves the expense and closes the form.
        /// </summary>
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

        /// <summary>Returns to the dashboard without saving.</summary>
        private void BtnBack_Click(object? sender, EventArgs e) => Close();
    }
}

using ExpenseSavingsTracker.Database;
using ExpenseSavingsTracker.Models;

namespace ExpenseSavingsTracker.Forms
{
    /// <summary>
    /// Displays all expenses for the current calendar month in a grid with a monthly total.
    /// </summary>
    public partial class ExpenseHistoryForm : Form
    {
        private readonly int _userId;

        public ExpenseHistoryForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadExpenses();
        }

        /// <summary>
        /// Loads this month's expenses from the database and binds them to the DataGridView.
        /// </summary>
        private void LoadExpenses()
        {
            var now = DateTime.Now;
            List<Expense> expenses = DatabaseHelper.GetExpenses(_userId, now.Year, now.Month);

            // Anonymous projection for friendly column display in the grid
            dgvExpenses.DataSource = expenses.Select(e => new
            {
                Date = e.ExpenseDate.ToString("dd MMM yyyy"),
                e.Category,
                Amount = $"Rs {e.Amount:N0}",
                e.Note
            }).ToList();

            decimal total = expenses.Sum(e => e.Amount);
            lblTotal.Text = $"Total this month: Rs {total:N0}";
        }

        /// <summary>Closes the form and returns to the dashboard.</summary>
        private void BtnBack_Click(object? sender, EventArgs e) => Close();
    }
}

using FinanceTracker.Database;
using FinanceTracker.Models;

namespace FinanceTracker.Forms
{
    public partial class ExpenseHistoryForm : Form
    {
        private readonly int _userId;

        public ExpenseHistoryForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadExpenses();
        }

        private void LoadExpenses()
        {
            var now = DateTime.Now;
            List<Expense> expenses = DatabaseHelper.GetExpenses(_userId, now.Year, now.Month);

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

        private void BtnBack_Click(object? sender, EventArgs e) => Close();
    }
}

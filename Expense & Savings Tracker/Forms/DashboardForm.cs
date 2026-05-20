using ExpenseSavingsTracker.Database;
using ExpenseSavingsTracker.Models;

namespace ExpenseSavingsTracker.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly int _userId;

        public DashboardForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            DashboardSummary summary = DatabaseHelper.GetDashboardSummary(_userId);
            lblWelcome.Text = $"Welcome, {summary.UserName}";
            lblExpenseAmount.Text = $"Rs {summary.MonthlyExpenses:N0}";
            lblSavingsAmount.Text = $"Rs {summary.TotalSavings:N0}";
            txtMonthlyBudget.Text = summary.MonthlyBudget > 0 ? summary.MonthlyBudget.ToString("0") : "";

            if (summary.MonthlyBudget <= 0)
            {
                progressBudget.Value = 0;
                lblBudgetStatus.Text = "Set a monthly budget to track spending.";
                lblBudgetStatus.ForeColor = Color.FromArgb(71, 85, 105);
                return;
            }

            int percent = summary.BudgetUsedPercent;
            progressBudget.Value = Math.Min(100, Math.Max(0, percent));

            if (summary.IsOverBudget)
            {
                lblBudgetStatus.Text = $"Over budget by Rs {Math.Abs(summary.BudgetRemaining):N0}!";
                lblBudgetStatus.ForeColor = Color.FromArgb(220, 38, 38);
                progressBudget.ForeColor = Color.FromArgb(220, 38, 38);
            }
            else
            {
                lblBudgetStatus.Text = $"Rs {summary.BudgetRemaining:N0} remaining ({percent}% used)";
                lblBudgetStatus.ForeColor = Color.FromArgb(5, 150, 105);
                progressBudget.ForeColor = Color.FromArgb(37, 99, 235);
            }
        }

        private void BtnSaveBudget_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonthlyBudget.Text.Trim(), out decimal budget) || budget < 0)
            {
                MessageBox.Show("Enter a valid monthly budget amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseHelper.UpdateMonthlyBudget(_userId, budget);
            MessageBox.Show("Monthly budget saved.", "Budget",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDashboard();
        }

        private void BtnAddExpense_Click(object? sender, EventArgs e)
        {
            using var form = new ExpenseForm(_userId);
            form.ShowDialog();
            LoadDashboard();
        }

        private void BtnExpenseHistory_Click(object? sender, EventArgs e)
        {
            using var form = new ExpenseHistoryForm(_userId);
            form.ShowDialog();
            LoadDashboard();
        }

        private void BtnSavings_Click(object? sender, EventArgs e)
        {
            using var form = new SavingsForm(_userId);
            form.ShowDialog();
            LoadDashboard();
        }

        private void BtnReports_Click(object? sender, EventArgs e)
        {
            using var form = new ReportsForm(_userId);
            form.ShowDialog();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadDashboard();

        private void BtnLogout_Click(object? sender, EventArgs e) => Close();
    }
}

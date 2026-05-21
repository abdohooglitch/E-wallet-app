using ExpenseSavingsTracker.Database;
using ExpenseSavingsTracker.Models;

namespace ExpenseSavingsTracker.Forms
{
    /// <summary>
    /// Main hub after login: budget tracking, navigation to expenses, savings, and reports.
    /// </summary>
    public partial class DashboardForm : Form
    {
        /// <summary>Logged-in user's Id passed to child forms and database calls.</summary>
        private readonly int _userId;

        public DashboardForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadDashboard();
        }

        /// <summary>
        /// Refreshes welcome text, monthly totals, and budget progress bar from the database.
        /// </summary>
        private void LoadDashboard()
        {
            DashboardSummary summary = DatabaseHelper.GetDashboardSummary(_userId);
            lblWelcome.Text = $"Welcome, {summary.UserName}";
            lblExpenseAmount.Text = $"Rs {summary.MonthlyExpenses:N0}";
            lblSavingsAmount.Text = $"Rs {summary.TotalSavings:N0}";
            txtMonthlyBudget.Text = summary.MonthlyBudget > 0 ? summary.MonthlyBudget.ToString("0") : "";

            // No budget set yet — show neutral message instead of progress
            if (summary.MonthlyBudget <= 0)
            {
                progressBudget.Value = 0;
                lblBudgetStatus.Text = "Set a monthly budget to track spending.";
                lblBudgetStatus.ForeColor = Color.FromArgb(71, 85, 105);
                return;
            }

            int percent = summary.BudgetUsedPercent;
            progressBudget.Value = Math.Min(100, Math.Max(0, percent));

            // Visual feedback: red when over budget, green when within limit
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

        /// <summary>Persists the monthly budget entered by the user and reloads the dashboard.</summary>
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

        /// <summary>Opens add-expense form; refreshes dashboard when user returns.</summary>
        private void BtnAddExpense_Click(object? sender, EventArgs e)
        {
            using var form = new ExpenseForm(_userId);
            form.ShowDialog();
            LoadDashboard();
        }

        /// <summary>Opens current month's expense list.</summary>
        private void BtnExpenseHistory_Click(object? sender, EventArgs e)
        {
            using var form = new ExpenseHistoryForm(_userId);
            form.ShowDialog();
            LoadDashboard();
        }

        /// <summary>Opens savings goals management screen.</summary>
        private void BtnSavings_Click(object? sender, EventArgs e)
        {
            using var form = new SavingsForm(_userId);
            form.ShowDialog();
            LoadDashboard();
        }

        /// <summary>Opens category-wise spending report for a chosen month.</summary>
        private void BtnReports_Click(object? sender, EventArgs e)
        {
            using var form = new ReportsForm(_userId);
            form.ShowDialog();
        }

        /// <summary>Reloads dashboard figures without reopening child forms.</summary>
        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadDashboard();

        /// <summary>Closes dashboard and returns user to the login form.</summary>
        private void BtnLogout_Click(object? sender, EventArgs e) => Close();
    }
}

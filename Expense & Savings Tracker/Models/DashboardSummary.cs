namespace ExpenseSavingsTracker.Models
{
    /// <summary>
    /// Aggregated financial overview displayed on the main dashboard.
    /// </summary>
    public class DashboardSummary
    {
        /// <summary>User's display name for the welcome label.</summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>Sum of all expenses in the current calendar month.</summary>
        public decimal MonthlyExpenses { get; set; }

        /// <summary>Sum of saved amounts across all savings goals.</summary>
        public decimal TotalSavings { get; set; }

        /// <summary>User's configured monthly spending limit.</summary>
        public decimal MonthlyBudget { get; set; }

        /// <summary>Budget minus expenses; negative when over budget.</summary>
        public decimal BudgetRemaining => MonthlyBudget - MonthlyExpenses;

        /// <summary>Percentage of monthly budget already spent (0–100).</summary>
        public int BudgetUsedPercent =>
            MonthlyBudget <= 0 ? 0 : (int)Math.Min(100, (MonthlyExpenses / MonthlyBudget) * 100);

        /// <summary>True when monthly expenses exceed the set budget.</summary>
        public bool IsOverBudget => MonthlyBudget > 0 && MonthlyExpenses > MonthlyBudget;
    }
}

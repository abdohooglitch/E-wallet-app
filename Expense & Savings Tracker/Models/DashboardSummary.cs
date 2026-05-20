namespace ExpenseSavingsTracker.Models
{
    public class DashboardSummary
    {
        public string UserName { get; set; } = string.Empty;
        public decimal MonthlyExpenses { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal MonthlyBudget { get; set; }

        public decimal BudgetRemaining => MonthlyBudget - MonthlyExpenses;

        public int BudgetUsedPercent =>
            MonthlyBudget <= 0 ? 0 : (int)Math.Min(100, (MonthlyExpenses / MonthlyBudget) * 100);

        public bool IsOverBudget => MonthlyBudget > 0 && MonthlyExpenses > MonthlyBudget;
    }
}

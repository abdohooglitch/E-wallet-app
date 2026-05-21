namespace ExpenseSavingsTracker.Models
{
    /// <summary>
    /// Summary of spending for one category within a selected month (used in reports).
    /// </summary>
    public class CategoryReport
    {
        /// <summary>Expense category name.</summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>Total amount spent in this category for the period.</summary>
        public decimal Total { get; set; }

        /// <summary>Share of total monthly spending as a percentage (0–100).</summary>
        public decimal Percentage { get; set; }
    }
}

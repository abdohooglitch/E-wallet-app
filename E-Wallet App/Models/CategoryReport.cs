namespace FinanceTracker.Models
{
    public class CategoryReport
    {
        public string Category { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public decimal Percentage { get; set; }
    }
}

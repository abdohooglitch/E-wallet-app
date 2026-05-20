namespace ExpenseSavingsTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public decimal MonthlyBudget { get; set; }
    }
}

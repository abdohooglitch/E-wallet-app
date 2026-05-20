using System;

namespace ExpenseSavingsTracker.Models
{
    public class SavingGoal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal SavedAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public decimal ProgressPercent =>
            TargetAmount <= 0 ? 0 : Math.Min(100, (SavedAmount / TargetAmount) * 100);

        public decimal Remaining => Math.Max(0, TargetAmount - SavedAmount);
    }
}

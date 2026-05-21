using System;

namespace ExpenseSavingsTracker.Models
{
    /// <summary>
    /// Represents a savings goal with a target amount and current saved balance.
    /// </summary>
    public class SavingGoal
    {
        /// <summary>Primary key for the savings goal.</summary>
        public int Id { get; set; }

        /// <summary>Foreign key linking this goal to the owning user.</summary>
        public int UserId { get; set; }

        /// <summary>Short title describing the goal (e.g. "New Laptop").</summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>Total amount the user wants to save.</summary>
        public decimal TargetAmount { get; set; }

        /// <summary>Amount already deposited toward this goal.</summary>
        public decimal SavedAmount { get; set; }

        /// <summary>Timestamp when the goal was created.</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>Completion percentage capped at 100%.</summary>
        public decimal ProgressPercent =>
            TargetAmount <= 0 ? 0 : Math.Min(100, (SavedAmount / TargetAmount) * 100);

        /// <summary>Amount still needed to reach the target (never negative).</summary>
        public decimal Remaining => Math.Max(0, TargetAmount - SavedAmount);
    }
}

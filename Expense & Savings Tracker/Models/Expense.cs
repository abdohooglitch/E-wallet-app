using System;

namespace ExpenseSavingsTracker.Models
{
    /// <summary>
    /// Represents a single expense transaction recorded by a user.
    /// </summary>
    public class Expense
    {
        /// <summary>Primary key for the expense record.</summary>
        public int Id { get; set; }

        /// <summary>Foreign key linking this expense to the owning user.</summary>
        public int UserId { get; set; }

        /// <summary>Spending category (e.g. Food, Transport).</summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>Amount spent in rupees.</summary>
        public decimal Amount { get; set; }

        /// <summary>Date when the expense occurred.</summary>
        public DateTime ExpenseDate { get; set; }

        /// <summary>Optional description or note for the expense.</summary>
        public string Note { get; set; } = string.Empty;
    }
}

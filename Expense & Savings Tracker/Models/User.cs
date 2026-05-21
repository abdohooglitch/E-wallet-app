namespace ExpenseSavingsTracker.Models
{
    /// <summary>
    /// Represents a registered user account stored in the database.
    /// </summary>
    public class User
    {
        /// <summary>Primary key for the user record.</summary>
        public int Id { get; set; }

        /// <summary>Display name shown on the dashboard welcome message.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Unique mobile number used for login and registration.</summary>
        public string MobileNumber { get; set; } = string.Empty;

        /// <summary>Password stored in plain text (course project; not for production use).</summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>Monthly spending limit set by the user on the dashboard.</summary>
        public decimal MonthlyBudget { get; set; }
    }
}

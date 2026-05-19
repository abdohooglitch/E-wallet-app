using System;

namespace EWalletApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; } // "Deposit", "Send", "Receive"
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; } // e.g., "From 123", "To 456", "Self Deposit"
    }
}

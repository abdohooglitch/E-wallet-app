using System;

namespace EWalletApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
    }
}

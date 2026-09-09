// Represents a single bank account (Everyday or Saver) belonging to a client

namespace XBankApp
{
    // Represents a single bank account (Everyday or Saver) belonging to a client 
    public class BankAccount
    {
        public string AccountType { get; set; }
        public decimal Balance { get; set; }

        // Initializes a new instance of the BankAccount class with the specified account type and an optional initial balance (default is 0)
        public BankAccount(string accountType, decimal initialBalance = 0)
        {
            AccountType = accountType;
            Balance = initialBalance;
        }
    }
}
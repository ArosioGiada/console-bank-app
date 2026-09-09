// Represents a registered bank client, holding their credentials and accounts
// This class contains methods to verify login credentials and manage the client's bank accounts
using System.Security.Cryptography;
using System.Text;

namespace XBankApp
{
    // Represents a registered bank client, holding their credentials and accounts
    public class BankClient
    {
        // Properties for the client's username, email, hashed password, age, phone number, and two bank accounts (Everyday and Saver)
        public string Username { get; private set; }
        public string Email { get; private set; }
        private string PasswordHash { get; set; }
        public int Age { get; private set; }
        public string Phone { get; private set; }
        public BankAccount EverydayAccount { get; private set; }
        public BankAccount SaverAccount { get; private set; }

        // Initializes a new instance of the BankClient class with the provided credentials and creates two bank accounts
        public BankClient(string username, string email, string password, int age, string phone)
        {
            Username = username;
            Email = email;
            PasswordHash = HashPassword(password);
            Age = age;
            Phone = phone;

            EverydayAccount = new BankAccount("Everyday");
            SaverAccount = new BankAccount("Saver");
        }

        // Verifies login credentials by comparing the hash of the entered password
        // (the plain-text password is never stored, only its hash)
        public bool VerifyCredentials(string loginInput, string password)
        {
            return (loginInput == Username || loginInput == Email) && PasswordHash == HashPassword(password);
        }

        // Hashes a password using SHA256 and returns the hexadecimal string representation of the hash
        private static string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
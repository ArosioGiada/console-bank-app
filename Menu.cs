// Represents the console menu system for the banking application
// This class contains static methods to display various menus and prompts to the user
using System;

namespace XBankApp
{
    // Holds all console menu display methods
    public static class Menu
    {
        public static void MainMenu()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Signup");
            Console.WriteLine("3. Quit");
            Console.Write("Select an option: ");
        }

        public static void AccountMenu()
        {
            Console.WriteLine("\n--- Account Menu ---");
            Console.WriteLine("1. View Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer Between Accounts");
            Console.WriteLine("5. Switch Account Type");
            Console.WriteLine("6. Logout");
            Console.Write("Select an option: ");
        }

        public static void AccountTypeMenu()
        {
            Console.WriteLine("\n--- Select Account Type ---");
            Console.WriteLine("1. Everyday Account");
            Console.WriteLine("2. Saver Account");
            Console.Write("Select an option: ");
        }

        public static void RetryMenu()
        {
            Console.WriteLine("\n1. Try Again");
            Console.WriteLine("2. Main Menu");
            Console.WriteLine("3. Quit");
            Console.Write("Select an option: ");
        }
    }
}
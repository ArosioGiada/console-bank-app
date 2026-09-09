// Represents the main program for the bank application
// It handles user interactions, login, signup, and account operations
// The program maintains a list of registered users and their accounts
// The program uses a console-based interface to interact with users, allowing them to perform various banking operations such as viewing balances, depositing, withdrawing, and transferring funds between accounts.

using System;
using System.Collections.Generic;

namespace XBankApp
{
    class Bank
    {
        // List to store registered users and their data
        static List<BankClient> users = new List<BankClient>();
        static BankClient? currentUser = null;
        static BankAccount? currentAccount = null;

        // Entry point of the application
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Welcome to X Bank App");
            Console.WriteLine("---------------------------------");

            // Credentials for example user, kept out of the list to avoid mixing it with real clients
            string exampleUsername = "Demo.User";
            string examplePassword = "Demo1234";
            BankClient exampleUser = new BankClient(exampleUsername, "demo.user@example.com", examplePassword, 30, "12345");
            users.Add(exampleUser);

            Console.WriteLine("(Tip: Signup or try the demo account — Username: Demo.User, Password: Demo1234)");

            bool exit = false;

            while (!exit)
            {
                // Display the main menu and prompt the user for their choice
                Menu.MainMenu();
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("----Invalid option selected. Please enter a number from 1 to 3----");
                    continue;
                }

                // Handle the selected option based on user input
                switch (option)
                {
                    // Handle the login option, allowing existing users to log in to their accounts
                    case 1:
                        int loginResult = Login();
                        if (loginResult == 1)
                        {
                            SelectAccountType();
                            AccountOperations();
                            continue;
                        }
                        else if (loginResult == 2)
                        {
                            continue;
                        }
                        else if (loginResult == 3)
                        {
                            exit = true;
                        }
                        break;

                    // Handle the signup option, allowing new users to register for an account
                    case 2:
                        Signup();
                        break;

                    // Handle the quit option, exiting the application with a goodbye message
                    case 3:
                        Console.WriteLine("\n---------------------------------");
                        Console.WriteLine("Goodbye, thank you for using X Bank App");
                        Console.WriteLine("---------------------------------");
                        exit = true;
                        break;

                    // Handle invalid options, prompting the user to select a valid option from the menu
                    default:
                        Console.WriteLine("----Invalid option selected. Please enter a number from 1 to 3----");
                        break;
                }
            }
        }
        // Allows the user to select which account type (Everyday or Saver) they want to operate on
        static void SelectAccountType()
        {
            // Ensure that a user is logged in before allowing account selection
            if (currentUser == null)
            {
                return;
            }
            // Loop until a valid account type is selected
            while (true)
            {
                // Display the account type selection menu and prompt the user for their choice
                Menu.AccountTypeMenu();
                if (!int.TryParse(Console.ReadLine(), out int accountTypeOption))
                {
                    Console.WriteLine("----Invalid option selected. Please enter option number 1 or 2----");
                    continue;
                }

                // Handle the selected account type based on user input
                switch (accountTypeOption)
                {
                    // Set the current account to the selected account type and display a confirmation message
                    case 1:
                        currentAccount = currentUser.EverydayAccount;
                        Console.WriteLine("Everyday account selected");
                        return;
                    case 2:
                        currentAccount = currentUser.SaverAccount;
                        Console.WriteLine("Saver account selected");
                        return;
                    default:
                        Console.WriteLine("\n----Invalid option selected. Please enter option number 1 or 2----");
                        break;
                }
            }
        }

        // Handles the operations that can be performed on the selected account, such as viewing balance, depositing, withdrawing, and transferring funds
        static void AccountOperations()
        {
            // Ensure that a user is logged in and an account is selected before allowing account operations
            if (currentAccount == null)
            {
                return;
            }

            bool exitAccountMenu = false;

            // Loop until the user chooses to exit the account operations menu
            while (!exitAccountMenu)
            {
                Console.WriteLine($"\nCurrent account: {currentAccount.AccountType}");

                // Display the account operations menu and prompt the user for their choice
                Menu.AccountMenu();
                if (!int.TryParse(Console.ReadLine(), out int accountOption))
                {
                    Console.WriteLine("----Invalid option selected. Please enter a number from 1 to 6----");
                    continue;
                }

                // Handle the selected account operation based on user input
                switch (accountOption)
                {
                    case 1:
                        ViewBalance();
                        break;
                    case 2:
                        Deposit();
                        break;
                    case 3:
                        Withdraw();
                        break;
                    case 4:
                        TransferBetweenAccounts();
                        break;
                    case 5:
                        SelectAccountType();
                        break;
                    case 6:
                        exitAccountMenu = true;
                        break;
                    default:
                        Console.WriteLine("----Invalid option selected. Please enter a number from 1 to 6----");
                        break;
                }
            }
        }

        // Displays the current balance of the selected account
        static void ViewBalance()
        {
            if (currentAccount == null)
            {
                return;
            }
            Console.WriteLine($"\nYour current balance is: {currentAccount.Balance:C}");
        }

        // Allows the user to deposit funds into the selected account
        static void Deposit()
        {
            if (currentAccount == null)
            {
                return;
            }

            // Prompt the user to enter the amount they wish to deposit, ensuring that the input is valid
            Console.Write("\n----- ATTENTION ----- Please use a dot (.) for decimal values");
            Console.Write("\nEnter the amount to deposit: ");
            string input = Console.ReadLine() ?? string.Empty;

            // Validate the deposit amount input to ensure it is a valid decimal number and greater than zero
            if (string.IsNullOrEmpty(input) || input.Contains(",") || !decimal.TryParse(input, out decimal depositAmount) || depositAmount <= 0)
            {
                Console.WriteLine("Deposit unsuccessful. The entered amount is invalid");
                return;
            }

            // Perform the deposit by adding the amount to the current account's balance
            currentAccount.Balance += depositAmount;
            Console.WriteLine($"Deposit successful. Your new balance is {currentAccount.Balance:C}.");
        }

        // Allows the user to withdraw funds from the selected account
        static void Withdraw()
        {
            if (currentAccount == null)
            {
                return;
            }
            // Prompt the user to enter the amount they wish to withdraw, ensuring that the input is valid and that sufficient funds are available
            Console.Write("\nEnter the amount to withdraw: ");
            string input = Console.ReadLine() ?? string.Empty;

            // Validate the withdrawal amount input to ensure it is a valid decimal number and greater than zero
            if (string.IsNullOrEmpty(input) || input.Contains(",") || !decimal.TryParse(input, out decimal withdrawAmount) || withdrawAmount <= 0)
            {
                Console.WriteLine("Withdrawal unsuccessful. The entered amount is invalid.");
                return;
            }

            // Check if the withdrawal amount exceeds the balance of the current account
            if (withdrawAmount > currentAccount.Balance)
            {
                Console.WriteLine("Withdrawal unsuccessful. Insufficient funds.");
                return;
            }

            // Perform the withdrawal by deducting the amount from the current account's balance
            currentAccount.Balance -= withdrawAmount;
            Console.WriteLine($"Withdrawal successful. Remaining balance: {currentAccount.Balance:C}.");
        }

        // Allows the user to transfer funds between their accounts
        static void TransferBetweenAccounts()
        {
            if (currentUser == null || currentAccount == null)
            {
                return;
            }

            // Determine the source and destination accounts for the transfer
            BankAccount fromAccount = currentAccount;
            BankAccount toAccount = fromAccount == currentUser.EverydayAccount
                ? currentUser.SaverAccount
                : currentUser.EverydayAccount;

            Console.WriteLine($"\nTransferring from {fromAccount.AccountType} to {toAccount.AccountType}");
            Console.Write("\n----- ATTENTION ----- Please use a dot (.) for decimal values");
            Console.Write("\nEnter the amount to transfer: ");
            string input = Console.ReadLine() ?? string.Empty;

            // Validate the transfer amount input to ensure it is a valid decimal number and greater than zero
            if (string.IsNullOrEmpty(input) || input.Contains(",") || !decimal.TryParse(input, out decimal transferAmount) || transferAmount <= 0)
            {
                Console.WriteLine("Transfer unsuccessful. The entered amount is invalid.");
                return;
            }
            
            // Check if the transfer amount exceeds the balance of the source account
            if (transferAmount > fromAccount.Balance)
            {
                Console.WriteLine("Transfer unsuccessful. Insufficient funds.");
                return;
            }

            // Perform the transfer by deducting from the source account and adding to the destination account
            fromAccount.Balance -= transferAmount;
            toAccount.Balance += transferAmount;
            Console.WriteLine($"Transfer successful. Transferred {transferAmount:C} to {toAccount.AccountType}.");
        }

        // Returns 1 for login success, 2 for return to main menu and 3 for exit
        static int Login()
        {
            int attempts = 3;

            Console.WriteLine("\nATTENTION: You have only a total of 3 attempts to login");

            // Loop until the user either successfully logs in or exhausts their attempts
            while (attempts > 0)
            {
                Console.WriteLine("\nEnter your username or email: ");
                string loginInput = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Enter your password: ");
                string password = Console.ReadLine() ?? string.Empty;

                // Check for empty credentials and prompt the user to try again if they are empty
                if (string.IsNullOrEmpty(loginInput) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Credentials cannot be empty. Please try again.");
                    continue;
                }
                // Attempt to find a user that matches the provided credentials
                BankClient? loggedInUser = users.Find(user => user.VerifyCredentials(loginInput, password));
                if (loggedInUser != null)
                {
                    Console.WriteLine($"\nWelcome {loggedInUser.Username}!");
                    currentUser = loggedInUser;
                    return 1;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Invalid username/email or password. You have {attempts} attempts left.");
                    if (attempts == 0)
                    {
                        Console.WriteLine("\nYou have exceeded the number of attempts.\n----Goodbye, thank you for using X Bank App----\n");
                        return 3;
                    }
                    // Prompt the user to retry, return to the main menu, or exit the application
                    while (true)
                    {
                        Menu.RetryMenu();
                        if (!int.TryParse(Console.ReadLine(), out int retryOption))
                        {
                            Console.WriteLine("----Invalid option selected. Please enter a number from 1 to 3----");
                            continue;
                        }
                        switch (retryOption)
                        {
                            case 1:
                                break;
                            case 2:
                                return 2;
                            case 3:
                                Console.WriteLine("----Goodbye, thank you for using X Bank App----");
                                return 3;
                            default:
                                Console.WriteLine("----Invalid option selected. Please enter a number from 1 to 3----");
                                continue;
                        }
                        break;
                    }
                }
            }
            return 2;
        }
        // Handles the signup process for new users, collecting their details and creating a new BankClient instance
        static void Signup()
        {
            // Variables to hold user input for the signup process
            string newUsername, newEmail, newPhone, newPassword;
            int newAge;

            // Loop until the user successfully signs up with valid details
            while (true)
            {
                Console.WriteLine("\nBank Registration");
                Console.Write("\nEnter Username: ");
                newUsername = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter Email: ");
                newEmail = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter Age: ");
                string newAgeInput = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter Phone: ");
                newPhone = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter Password: ");
                newPassword = Console.ReadLine() ?? string.Empty;

                // Validate that all fields are filled and that the age is a valid number
                if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(newEmail) || string.IsNullOrEmpty(newAgeInput) || string.IsNullOrEmpty(newPhone) || string.IsNullOrEmpty(newPassword))
                {
                    Console.WriteLine("All fields are required. Please enter all details correctly.");
                    continue;
                }
                if (!int.TryParse(newAgeInput, out newAge))
                {
                    Console.WriteLine("Invalid age. Please enter a valid number.");
                    continue;
                }
                if (users.Exists(user => user.Username == newUsername || user.Email == newEmail))
                {
                    Console.WriteLine("Username or email already exists. Please try again.");
                    continue;
                }

                // Create a new BankClient instance with the provided details and add it to the list of users
                BankClient newUser = new BankClient(newUsername, newEmail, newPassword, newAge, newPhone);
                users.Add(newUser);

                Console.WriteLine("\nYou have successfully signed up. Welcome to X Bank.");
                break;
            }
        }
    }
}
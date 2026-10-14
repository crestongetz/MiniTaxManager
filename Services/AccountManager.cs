using System;
using System.Collections.Generic;
using TaxManager.Models;

namespace TaxManager.Services
{
    public class AccountManager
    {
        private readonly Dictionary<int, TaxAccount> _accounts = new();
        private readonly Dictionary<string, Action> _menu;
        private int _nextId = 1;

        public AccountManager()
        {
            _menu = new Dictionary<string, Action>
            {
                ["1"] = CreateAccount,
                ["2"] = AssessTax,
                ["3"] = RecordPayment,
                ["4"] = ApplyLateFee,
                ["5"] = ViewAccount,
                ["6"] = ShowReports
            };
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                ShowMenu();
                string userChoice = Console.ReadLine()?.Trim() ?? "";

                if (userChoice == "0") running = false; // If user inputs 0 end program
                else if (_menu.TryGetValue(userChoice, out Action? action)) action(); // Try and get user action and execute it
                else Console.WriteLine("Invalid choice. Please try again."); // catch all
            }
        }
    
        // Create a new account and store into dictionary
        // TODO: Change to add account to a database
        public void CreateAccount()
            {
                // get owner name
                string Owner = ReadText("Enter owner name: ", 2, 50);

                Console.WriteLine("Account Type: ");
                Console.WriteLine("1. Individual");
                Console.WriteLine("2. Business");
                // Get user choice
                string accountType = ReadChoice("Choose type: ", "1", "2"); 

                TaxAccount account = accountType == "1" 
                    ? new IndividualAccount(_nextId, Owner)
                    : new BusinessAccount(_nextId, Owner);

                _accounts[account.Id] = account;
                _nextId++;

                Console.WriteLine($"Account Created: {account.Describe()}");
            }

        public void AssessTax()
        {
            TaxAccount? account = SearchAccount(); // null if not found
            if (account is null)
            { 
                Console.WriteLine("Account not found.");
                return;
            }

            decimal amount = ReadAmount("Enter tax amount to assess: ");
            string note = ReadText("Enter note for assessment: ", 0, 150);

            account.AssessTax(amount, note);
            Console.WriteLine($"Tax assessed: {amount:C} for account #{account.Id}. New balance: {account.BalanceOwed:C}");
        }

        public void RecordPayment()
        {
            TaxAccount? account = SearchAccount(); // null if not found
            if (account is null)
            { 
                Console.WriteLine("Account not found.");
                return;
            }

            decimal amount = ReadAmount("Enter payment amount: ");
            string note = ReadText("Enter note for payment: ", 0, 150);

            account.ApplyPayment(amount, note);
            Console.WriteLine($"Payment recorded: {amount:C} for account #{account.Id}. New balance: {account.BalanceOwed:C}");      
        }

        // Applies a penalty to the account on their balanced owed based on account type
        // Method has no check for double charging, in a production system checks would be needed. 
        public void ApplyLateFee()
        {
            TaxAccount? account = SearchAccount();
            if (account is null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            decimal penalty = account.ApplyPenalty("Late fee applied");

            if (penalty > 0) Console.WriteLine("No balance owed. No penalty can be applied");
            else 
            {
                Console.WriteLine($"Penalty of {penalty:C} applied to account #{account.Id}");
                Console.WriteLine($"New balance: {account.BalanceOwed:C}");
            }


        }

        public void ViewAccount()
        {
            TaxAccount? account = SearchAccount();
            if (account is null) return;
            Console.WriteLine(account.Describe());

            // Show transactions
        }

        public void ShowReports()
        {
            //TODO
        }


        public static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== Tax Account Manager ===");
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Assess tax");
            Console.WriteLine("3. Record payment");
            Console.WriteLine("4. Apply late penalties");
            Console.WriteLine("5. View account");
            Console.WriteLine("6. Reports");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
        }

        // Searchs for account by Id and returns account, null if not found
        private TaxAccount? SearchAccount(int? accountId = null)
            {
                TaxAccount? account = null;

                if (!accountId.HasValue)
                {
                    Console.Write("Enter account ID: ");
                    string input = Console.ReadLine()?.Trim() ?? "";

                    if (!int.TryParse(input, out int id))
                    {
                        Console.WriteLine("Invalid account Id.");
                        return null;
                    }
                    accountId = id;
                }

                if (!_accounts.TryGetValue(accountId.Value, out account))
                {
                    Console.WriteLine("Invalid account Id.");
                    return null;
                }

                if (!_accounts.TryGetValue(accountId.Value, out account))
                {
                    Console.WriteLine($"Account with ID {accountId.Value} not found.");
                    return null;
                }
                return account;
            }

        // Re-prompts until the trimmed input is between minLength and maxLength characters
        private static string ReadText(string prompt, int minLength, int maxLength)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";
                if (input.Length >= minLength && input.Length <= maxLength) return input;
                Console.WriteLine($"Please enter between {minLength} and {maxLength} characters.");
            }
        }

        // Re-prompts until the trimmed input is one of the allowed choices
        private static string ReadChoice(string prompt, params string[] allowed)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";
                if (Array.IndexOf(allowed, input) >= 0) return input;
                Console.WriteLine($"Please enter one of: {string.Join(", ", allowed)}");
            }
        }
        private static decimal ReadAmount(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value > 0)
                    return value;
                Console.WriteLine("Please enter a positive number.");
            }
        }


    }
}
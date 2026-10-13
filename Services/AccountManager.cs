using System;
using System.Collections.Generic;
using TaxManager.Models;

namespace TaxManager.Services
{
    public class AccountManager
    {
        private readonly Dictionary<int, TaxAccount> _accounts;
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
    
    public void CreateAccount()
        {
            
        }

    public void AssessTax()
        {
            
        }

    public void RecordPayment()
        {
            
        }

    public void ApplyLateFee()
        {
            
        }

    public void ViewAccount()
        {
            
        }

    public void ShowReports()
        {
            
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
    }
}
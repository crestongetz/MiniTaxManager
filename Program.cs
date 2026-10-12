using System;

namespace TaxManager
{
    // Entry Point of tax manager application
    class Program
    {
        static void Main(string[] arga)
        {
            Console.WriteLine("Welcome to the Tax Manager Application!");

            bool running = true;

            // Main loop
            while (running)
            {
                //ShowMenu();
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        //CreateAccount();
                        break;
                    case "2":
                        //AssessTax();
                        break;
                    case "3":
                        //RecordPayment();
                        break;
                    case "4":
                        //ApplyLateFee();
                        break;
                    case "5":
                        //ViewAccount();
                        break;
                    case "6":
                        //ShowReports();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}

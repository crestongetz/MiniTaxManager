using System;
using TaxManager.Services;

namespace TaxManager
{
    // Entry Point of tax manager application
    class Program
    {
        static void Main(string[] arga)
        {
            Console.WriteLine("Welcome to the Tax Manager Application!");
            new AccountManager().Run();

          
        }
    }
}

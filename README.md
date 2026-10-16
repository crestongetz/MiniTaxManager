# Tax Manager

A C# console app for managing tax accounts. Create individual or business accounts, assess taxes with due dates, record payments, and apply late penalties on overdue balances.

https://github.com/user-attachments/assets/8f092b04-57d6-4511-93b9-bf3a46bbe123

## Features

- Individual and business accounts, each with its own penalty rate
- Tax assessments with due dates
- Payments applied to the oldest assessments first
- Late penalties charged only on the overdue, unpaid amount
- Transaction history for each account
- Reports, including total owed across all accounts

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Running

```bash
git clone https://github.com/<your-username>/TaxManager.git
cd TaxManager
dotnet run
```

## Project Structure

```
Models/      Accounts, transactions, and the reporting interface
Services/    Menu, input handling, and reports
Program.cs   Entry point
```

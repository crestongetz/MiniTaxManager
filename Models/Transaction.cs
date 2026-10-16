using System;
namespace TaxManager.Models

{
    //record representing a financial transaction
    public record Transaction(DateTime Date, TransactionType Type, decimal Amount, string Note, 
        DateTime? DueDate = null);

}
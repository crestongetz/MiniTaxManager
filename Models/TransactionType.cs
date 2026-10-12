namespace TaxManager.Models
{
    //enum representing different types of transactions
    // Helps prevent invalid transaction types
    public enum TransactionType
    {
        Assessment,
        Payment,
        Penalty
    }
}
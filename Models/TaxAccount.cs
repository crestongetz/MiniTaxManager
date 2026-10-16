using System;
using System.Collections.Generic;
namespace TaxManager.Models
{
    //abstract class representing a tax account
    public abstract class TaxAccount
    {
        public int Id { get; }
        public string Owner { get;}
        public decimal BalanceOwed { get; private set;}
        
        private readonly List<Transaction> _transactions = new(); // Only class can modify
        public IReadOnlyList<Transaction> Transactions => _transactions;

        // subclasses must implment with override
        public abstract decimal PenaltyRate { get;}

        // subclasses optionally implement with override
        public virtual string Describe() => $"Tax Account for {Owner} (ID: {Id}) (Balance: {BalanceOwed:C}) (Penalty Rate: {PenaltyRate:P0}) (Next Due Date: {NextDueDate?.ToString("d") ?? "N/A"})";

        // TODO: add logic to determine payments
        public DateTime? NextDueDate => Transactions
        .Where(t => t.Type == TransactionType.Assessment && t.DueDate >= DateTime.Today)
        .Min(t => t.DueDate);

        // Constructor
        protected TaxAccount(int id, string owner)
        {
            Id = id;
            Owner = owner;
        }


        public void AssessTax(decimal amount, string note, DateTime? dueDate)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive value.", nameof(amount));
            if ( dueDate is null || dueDate.Value.Date <= DateTime.Now) 
                throw new ArgumentException("Invalid due date.", nameof(dueDate));

            BalanceOwed += amount;
            _transactions.Add(new Transaction(DateTime.Now, TransactionType.Assessment, amount, note, dueDate));
        }

        public void ApplyPayment(decimal amount, string note)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive value.", nameof(amount));

            BalanceOwed -= amount;
            _transactions.Add(new Transaction(DateTime.Now, TransactionType.Payment, amount, note));
        }

        // Applies a penalty to the account
        // Payments are not linked to specific assessments, next due date is the earliest date
        public decimal? ApplyPenalty(string note)
        {
            if (BalanceOwed <= 0) return null; // no penalty if balance is zero or negative

            var overdueTransactions = Transactions.Where(t => t.Type == TransactionType.Assessment && t.DueDate < DateTime.Today).ToList(); // finds overdue transactions
            if (!overdueTransactions.Any()) return 0; // no penalty if no overdue transactions

            // Calculates the amount that has already been paid
            decimal totalPaid = Transactions.Where(t => t.Type == TransactionType.Payment)
            .Sum(t => t.Amount);

            decimal overdueUnpaid = 0;

            foreach (var a in Transactions
            .Where(t=> t.Type == TransactionType.Assessment)
            .OrderBy(t => t.DueDate))
                {
                    decimal covered = Math.Min(a.Amount, totalPaid); // How much payments cover this assessment
                    totalPaid -= covered;

                    if (a.DueDate < DateTime.Today) // Only overdue assessments count for penalty
                        overdueUnpaid += a.Amount - covered;
                }
            if (overdueUnpaid <= 0) return 0;


            // Applies penalty to only overdue amount not entire balance.
            decimal penalty = Math.Round(overdueUnpaid * PenaltyRate, 2);


            BalanceOwed += penalty;
            _transactions.Add(new Transaction(DateTime.Now, TransactionType.Penalty, penalty, note));
            return penalty;
        }

        
    }
}
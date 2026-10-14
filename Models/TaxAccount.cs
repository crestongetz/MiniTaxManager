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
        public List<Transaction> Transactions { get; } = new();

        // subclasses must implment with override
        public abstract decimal PenaltyRate { get;}

        // subclasses optionally implement with override
        public virtual string Describe() => $"Tax Account for {Owner} (ID: {Id}) (Balance: {BalanceOwed:C})";

        // Constructor
        protected TaxAccount(int id, string owner)
        {
            Id = id;
            Owner = owner;
        }


        public void AssessTax(decimal amount, string note)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive value.", nameof(amount));

            BalanceOwed += amount;
            Transactions.Add(new Transaction(DateTime.Now, TransactionType.Assessment, amount, note));

        }

        public void ApplyPayment(decimal amount, string note)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive value.", nameof(amount));

            BalanceOwed -= amount;
            Transactions.Add(new Transaction(DateTime.Now, TransactionType.Payment, amount, note));
        }

        public decimal ApplyPenalty(string note)
        {
            if (BalanceOwed <= 0) return 0; // no penalty if balance is zero or negative

            decimal penalty = BalanceOwed * PenaltyRate;
            BalanceOwed += penalty;
            Transactions.Add(new Transaction(DateTime.Now, TransactionType.Penalty, penalty, note));
            return penalty;
        }

        
    }
}
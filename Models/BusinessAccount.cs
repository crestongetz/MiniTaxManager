using System;
using System.Collections.Generic;

namespace TaxManager.Models
{
    public class BusinessAccount : TaxAccount
    {
        // Constructor
        public BusinessAccount(int id, string owner) : base(id, owner) { }

        public override decimal PenaltyRate => 0.10m;

        public override string Describe()
        {
        return $"""
            Business account #{Id} for {Owner}
            Owes: {BalanceOwed:C}
            Penalty rate: {PenaltyRate:P0}
            Next due date: {NextDueDate?.ToString("d") ?? "N/A"}
            """;
        }
    }
}
using System;
using System.Collections.Generic;

namespace TaxManager.Models
{
    public class IndividualAccount : TaxAccount
    {
        // Constructor
        public IndividualAccount(int id, string owner) : base(id, owner) { }

        public override decimal PenaltyRate => 0.05m;

        public override string Describe()
        {

        return $"""
            Individual account #{Id} for {Owner}
            Owes: {BalanceOwed:C}
            Penalty rate: {PenaltyRate:P0}
            Next due date: {NextDueDate?.ToString("d") ?? "N/A"}
            """;

        }
    }
}

using System;
using Domain.ValueObjects;

namespace Domain
{
    public class Transaction
    {
        public readonly Money Amount;
        public readonly Percent InterestRate;

        public Transaction(Money amount, Percent interestRate)
        {
            _ = amount ?? throw new ArgumentNullException(nameof(amount));
            _ = interestRate ?? throw new ArgumentNullException(nameof(interestRate));

            Amount = amount;
            InterestRate = interestRate;
        }

        public Money FinanceCharge(Account account, Percent primeInterestRate, Percent taxRate) 
            => Amount + (account.AdminFee + (account.AdminFee * taxRate)) * ((InterestRate + primeInterestRate) / 365) * account.Term.Days;
    }
}

using System;
using Domain.ValueObjects;

namespace Domain
{
    public class Transaction
    {
        public Money Amount { get; }
        public Percent InterestRate { get; }

        public Transaction(Money amount, Percent interestRate)
        {
            _ = amount ?? throw new ArgumentNullException(nameof(amount));
            _ = interestRate ?? throw new ArgumentNullException(nameof(interestRate));

            Amount = amount;
            InterestRate = interestRate;
        }

        public Money FinanceCharge(Account account, Percent primeInterestRate, Percent taxRate)
        {
            _ = account ?? throw new ArgumentNullException(nameof(account));
            _ = primeInterestRate ?? throw new ArgumentNullException(nameof(primeInterestRate));
            _ = taxRate ?? throw new ArgumentNullException(nameof(taxRate));

            return (Amount + (account.AdminFee + account.AdminFee * taxRate)) * ((InterestRate + primeInterestRate) / 365) * account.Term.Days;
        }
    }
}

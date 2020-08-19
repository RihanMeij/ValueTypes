using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; }

        public Currency Currency { get; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public static Money operator *(Money money, Percent percent)
        {
            _ = money ?? throw new ArgumentNullException(nameof(money));
            _ = percent ?? throw new ArgumentNullException(nameof(percent));

            var amount = money.Amount * percent.Fraction;
            return new Money(amount, money.Currency);
        }

        public static Money operator +(Money first, Money second)
        {
            _ = first ?? throw new ArgumentNullException(nameof(first));
            _ = second ?? throw new ArgumentNullException(nameof(second));

            if (first.Currency != second.Currency)
            {
                throw new ArgumentOutOfRangeException(nameof(first),"Currency has to match");
            }
            return new Money(first.Amount + second.Amount, first.Currency);
        }

        public static Money operator *(Money money, decimal amount)
        {
            _ = money ?? throw new ArgumentNullException(nameof(money));

            return new Money(money.Amount * amount, money.Currency);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

    }
}
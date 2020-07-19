using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public readonly decimal Amount;

        public readonly Currency Currency;

        public Money(decimal amount)
        {
            Amount = amount;
            Currency = Currency.ZAR;
        }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public static Money operator *(Money money, Percent percent)
        {
            var amount = money.Amount * percent.Fraction;
            return new Money(amount, money.Currency);
        }

        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
            {
                throw new ArgumentOutOfRangeException(nameof(first),"Currency has to match");
            }
            return new Money(first.Amount + second.Amount, first.Currency);
        }

        public static Money operator *(Money money, decimal amount)
        {
            return new Money(money.Amount * amount, money.Currency);
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
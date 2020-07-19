using System;
using System.Collections.Generic;
namespace Domain.ValueObjects
{
    public class Rate : ValueObject
    {
        public readonly decimal Value;

        public Percent AsPercent => new Percent(Value * 100);

        public Rate(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Rate has to be a positive number");
            }

            Value = value;
        }

        public Rate(Percent percent) : this(percent.Value * 100)
        {
        }


        public Rate(decimal? value) : this(value ?? default)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static decimal operator *(Rate rate, decimal amount)
        {
            return rate.Value * amount;
        }

        public static Rate operator +(Rate a, Rate b)
        {
            return new Rate(a.Value + b.Value);
        }

        public static bool operator >(Rate left, Rate right)
        {
            var leftSide = left ?? new Rate(default(decimal));
            var rightSide = right ?? new Rate(default(decimal));

            return leftSide.Value > rightSide.Value;
        }

        public static bool operator <(Rate left, Rate right)
        {
            var leftSide = left ?? new Rate(default(decimal));
            var rightSide = right ?? new Rate(default(decimal));

            return leftSide.Value < rightSide.Value;
        }
    }
}

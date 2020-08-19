using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Domain.ValueObjects
{
    /// <summary>
    /// This is the fraction
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class Percent : ValueObject
    {
        /// <summary>
        /// Numerical represents the percent
        /// 10 % = 10
        /// </summary>
        public decimal Value { get;}

        /// <summary>
        /// The fraction that is a percent
        /// 10 % = 0.01
        /// </summary>
        public decimal Fraction => Value / 100;

        public Percent(decimal value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Percent operator +(Percent a, Percent b)
        {
            _ = a ?? throw new ArgumentNullException(nameof(a));
            _ = b ?? throw new ArgumentNullException(nameof(b));

            return new Percent(a.Value + b.Value);
        }

        public static Percent operator /(Percent percent, int denominator)
        {
            _ = percent ?? throw new ArgumentNullException(nameof(percent));

            if (denominator == 0)
            {
                throw new DivideByZeroException(nameof(denominator));
            }

            return new Percent(percent.Value / denominator);
        }

        public static Percent operator *(Percent percent, int multiplier)
        {
            _ = percent ?? throw new ArgumentNullException(nameof(percent));

            return new Percent(percent.Value * multiplier);
        }
    }
}

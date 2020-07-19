using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Domain.ValueObjects
{
    [DebuggerDisplay("{Value}")]
    public class Tax : ValueObject
    {
        public decimal Value { get; private set; }

        public Tax(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Domain.ValueObjects
{
    /// <summary>
    /// This is the work of Vladimr
    /// https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
    /// and Steven Roberts
    /// </summary>
    public abstract class ValueObject
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const int PrimeNumber = 23;
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * PrimeNumber + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }
        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}

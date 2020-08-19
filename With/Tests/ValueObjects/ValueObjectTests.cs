using System.Collections.Generic;
using System.Linq;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ValueObjects
{
    [TestClass]
    public class ValueObjectTests
    {
        [TestMethod]
        public void ObjectsAreEqual()
        {
            var objectA = new ValueObjectTestObject("A", 4);
            var objectB = new ValueObjectTestObject("A", 5);

            Assert.IsTrue(objectA == objectB);
            Assert.IsTrue(objectA.Equals(objectB));
        }

        [TestMethod]
        public void CheckHashCodeDistributionOnThousandItems()
        {
            var numberedList = new List<SimpleValueObject>();

            for (int i = 0; i < 1000; i++)
            {
                numberedList.Add(new SimpleValueObject(i));
            }

            var distinctHashCodes = numberedList.Select(x => x.GetHashCode()).Distinct();
            Assert.IsTrue(distinctHashCodes.Count()/1000 > 0.5d);
        }

    }

    internal class ValueObjectTestObject : ValueObject
    {
        public string Name { get; }
        public int RandomId { get; }

        public ValueObjectTestObject(string name, int id)
        {
            Name = name;
            RandomId = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }

    internal class SimpleValueObject : ValueObject
    {
        public int Number { get; }

        public SimpleValueObject(int number)
        {
            Number = number;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}

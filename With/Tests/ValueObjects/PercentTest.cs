using System.Collections.Generic;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ValueObjects
{
    [TestClass]
    public class PercentTest
    {

        [DataTestMethod]
        [DynamicData(nameof(GetValidPercents),DynamicDataSourceType.Method)]
        public void CreatePercent(object validValue)
        {
            var percent = new Percent((decimal)validValue);
            Assert.IsNotNull(percent);
        }

        static IEnumerable<object[]> GetValidPercents()
        {
            yield return new object[]
            {
                40M
            };
            yield return new object[]
            {
                -40M
            };
            yield return new object[]
            {
                10000M
            };
            yield return new object[]
            {
                0.40M
            };
        }

        [TestMethod]
        public void Adding()
        {
            var tenPercent = new Percent(10);
            var fortyPercent = new Percent(40);

            var total = tenPercent + fortyPercent;
            Assert.IsTrue(total.Value == 50M);
        }

        [TestMethod]
        public void DividePercentByNumber()
        {
            var tenPercent = new Percent(100);
            int denominator = 2;

            var result = tenPercent / denominator;

            Assert.IsTrue(result.Value == 50M);

        }
    }
}

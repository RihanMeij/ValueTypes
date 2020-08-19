using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ValueObjects
{
    [TestClass]
    public class MoneyTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetValidMoneyValues), DynamicDataSourceType.Method)]
        public void CreatePercent(dynamic validValue)
        {
            var money = new Money(validValue.Money, validValue.Currency);
            Assert.IsNotNull(money);
        }

        static IEnumerable<object[]> GetValidMoneyValues()
        {
            yield return new object[]
            {
                new {Money = 40M,
                    Currency = Currency.AUD }
            };
            yield return new object[]
            {
                new {Money = 0.3M,
                    Currency = Currency.AUD }
            };
            yield return new object[]
            {
                new {Money = -200M,
                    Currency = Currency.ZAR }
            };
        }
        
        [TestMethod]
        public void MoneyMultipliedByPercent()
        {
            var money = new Money(10,Currency.AUD);
            var percent = new Percent(10);
            var result = money * percent;
            Assert.IsTrue(result.Currency == Currency.AUD);
            Assert.IsTrue(result.Amount == 1.0M);
        }

        [TestMethod]
        public void AddingMoneyToMoney()
        {
            var firstAmount = new Money(10, Currency.AUD);
            var secondAmount = new Money(2,Currency.AUD);

            var result = firstAmount + secondAmount;

            Assert.IsTrue(result.Amount == 12M);
        }

        [TestMethod]
        public void MultiplyMoneyByDecimal()
        {
            var firstAmount = new Money(10, Currency.AUD);

            var result = firstAmount * 3;

            Assert.IsTrue(result.Amount == 30M);
        }
    }
}

using System;
using Domain;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TransactionTests
    {
        /// <summary>
        /// These values have been supplied by the domain expert
        /// </summary>
        [TestMethod]
        public void FinanceCharge()
        {
            var account = new Account(
                new Money( 500,Currency.ZAR),
                TimeSpan.FromDays(118),
                new Money(500,Currency.ZAR) 
                );
            
            var transaction = new Transaction(
                new Money(625913.48m, Currency.ZAR), 
                new Percent(2m));

            var financeCharge = transaction.FinanceCharge(account, new Percent(10.25m), new Percent(15));

            Assert.IsTrue(financeCharge.Amount == 24810.660214794520547945202846M);
        }
    }
}

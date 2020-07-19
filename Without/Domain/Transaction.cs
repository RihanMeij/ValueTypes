using System;
using System.Collections.Generic;

namespace Domain
{
    public class Transaction
    {
        public string TransactionId { get; private set; }
        public decimal? TransactionAmount { get; private set; }
        public decimal? TransactionBankRate { get; private set; }
        public string TransactionCurrency { get; private set; }
        public int? TransactionMargin { get; private set; }
        public DateTime? TransactionValueDate { get; private set; }

        public TransactionCalculationValue CalculateTransactionFinanceFee(Account account, Rate primeRate, Rate vatRate)
        {
            var isTransactionFinanceFeeCalculatable = IsTransactionFinanceFeeCalculatable(account, primeRate, vatRate);
            decimal? transactionFinanceFee = default(decimal?);

            if (isTransactionFinanceFeeCalculatable.isCalculatable)
            {
                decimal transactionMargin = (decimal)TransactionMargin.Value;
                var clientRandValue = Math.Round(TransactionAmount.Value * (TransactionBankRate.Value + (transactionMargin / Constants.CalculationConstants.BasisPointDivisor)), 2);

                DateTime calculationDate = TransactionValueDate.Value.AddDays(Convert.ToDouble(account.AccountTerm.Value));
                DateTime transactionSettlementDate = new DateTime(calculationDate.Year, calculationDate.Month, DateTime.DaysInMonth(calculationDate.Year, calculationDate.Month));
                DateTime transactionValueDate = new DateTime(TransactionValueDate.Value.Year, TransactionValueDate.Value.Month, TransactionValueDate.Value.Day);
                decimal financeDays = (transactionSettlementDate - transactionValueDate).Days + 1;
                decimal financeChargeRate = (primeRate.RatePercentage.Value + account.AccountFinanceFee.Value) / 365 * financeDays;

                transactionFinanceFee = (clientRandValue + (account.AccountAdminFee.Value * (vatRate.RatePercentage.Value / Constants.CalculationConstants.PercentageDivisor))) * financeChargeRate / Constants.CalculationConstants.PercentageDivisor;
                transactionFinanceFee = Math.Round(transactionFinanceFee.Value, 2);
            }
            return new TransactionCalculationValue(Constants.PropertyNames.TransactionFinanceFee, isTransactionFinanceFeeCalculatable.errorMessages, transactionFinanceFee);
        }

        private (bool isCalculatable, List<string> errorMessages) IsTransactionFinanceFeeCalculatable(Account account, Rate primeRate, Rate vatRate)
        {
            bool isCalculatable = true;

            List<string> errorMessages = new List<string>();

            if (!TransactionValueDate.HasValue) { errorMessages.Add("Requires Transaction Value Date. "); };
            if (!TransactionAmount.HasValue) { errorMessages.Add("Requires Transaction Amount. "); };
            if (!TransactionBankRate.HasValue) { errorMessages.Add("Requires Transaction Bank Rate. "); };
            if (!TransactionMargin.HasValue) { errorMessages.Add("Requires Transaction Margin. "); };
            if (!primeRate.RatePercentage.HasValue) { errorMessages.Add("Requires Valid Prime Rate. "); };
            if (!account.AccountFinanceFee.HasValue) { errorMessages.Add("Requires Account Finance Fee. "); };
            if (!account.AccountTerm.HasValue) { errorMessages.Add("Requires Account Term. "); };
            if (!account.AccountAdminFee.HasValue) { errorMessages.Add("Requires Account Admin Fee. "); };
            if (!vatRate.RatePercentage.HasValue) { errorMessages.Add("Requires Valid VAT Rate. "); };

            if (errorMessages.Count > 0)
            {
                isCalculatable = false;
            }

            return (isCalculatable, errorMessages);
        }
    }
}

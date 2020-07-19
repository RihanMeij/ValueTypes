using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Domain
{
    public class Constants
    {
        public static class PropertyNames
        {
            public const string TransactionFinanceFee = "TransactionFinanceFee";
        }

        public static class CalculationConstants
        {
            public const int BasisPointDivisor = 10000;
            public const int PercentageDivisor = 100;
        }
    }
}

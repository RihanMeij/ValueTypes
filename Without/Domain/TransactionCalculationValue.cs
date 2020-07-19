using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TransactionCalculationValue
    {
        public TransactionCalculationValue(string propertyName, List<string> errorMessages, dynamic value)
        {
            PropertyName = propertyName;
            Value = value;
            ErrorMessages = errorMessages;
        }

        /// <summary>
        /// The name of the property being calculated
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The value applicable to the property
        /// </summary>
        public dynamic Value { get; private set; }

        /// <summary>
        /// List of messages for fields that prevent the Transaction calculation
        /// </summary>
        public List<string> ErrorMessages { get; private set; }
    }
}

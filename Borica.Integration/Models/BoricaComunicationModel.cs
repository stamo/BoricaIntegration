using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Borica.Integration.Enumerations;

namespace Borica.Integration.Models
{
    /// <summary>
    /// Author: Stamo Petkov
    /// Created: 08.05.2016
    /// Description: Base model for communication with Borica
    /// </summary>
    public abstract class BoricaComunicationModel
    {
        private BoricaTransactionType transactionType;

        private string orderNumber;

        private int amount;

        private string terminalId;

        /// <summary>
        /// Type of the transaction to perform
        /// </summary>
        public BoricaTransactionType TransactionType
        {
            get
            {
                return this.transactionType;
            }

            set
            {
                this.transactionType = value;
            }
        }

        /// <summary>
        /// Unique order identifier. 
        /// Maximum 15 symbols in range ASCII code 33 - 126.
        /// </summary>
        public string OrderNumber
        {
            get
            {
                return this.orderNumber;
            }

            set
            {
                StringValidator(value, 15, "OrderNumber");
                this.orderNumber = String.Format("{0,-15}", value);
            }
        }

        /// <summary>
        /// Transaction ammount in cents.
        /// </summary>
        public int Amount
        {
            get
            {
                return this.amount;
            }

            set
            {
                this.amount = value;
            }
        }

        /// <summary>
        /// Virtual POS Identifier 
        /// Maximum 8 symbols in range ASCII code 33 - 126.
        /// </summary>
        public string TerminalId
        {
            get
            {
                return this.terminalId;
            }

            set
            {
                StringValidator(value, 8, "TerminalId");
                this.terminalId = String.Format("{0,-8}", value);
            }
        }

        /// <summary>
        /// Validates string properties
        /// </summary>
        /// <param name="value">Property value</param>
        /// <param name="maxLength">Maximum length of the property</param>
        /// <param name="propertyName">Name of the property</param>
        /// <returns></returns>
        protected bool StringValidator(string value, int maxLength, string propertyName)
        {
            bool result = true;

            if (value == null)
            {
                throw new ArgumentNullException(String.Format("The property \"{0}\" can not be null!",
                    propertyName));
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(String.Format("The maximum length of \"{0}\" is {1}!",
                    propertyName, maxLength));
            }

            if (value.Any(c => (int)c < 32 || (int)c > 126))
            {
                throw new ArgumentOutOfRangeException(String
                    .Format("Only symbols with ASCII code between 33 and 126 can be used for property \"{0}\"!",
                    propertyName));
            }

            return result;
        }
    }
}

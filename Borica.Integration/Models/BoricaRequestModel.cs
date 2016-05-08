using System;
using System.Linq;
using Borica.Integration.Constants;
using Borica.Integration.Enumerations;

namespace Borica.Integration.Models
{
    /// <summary>
    /// Author: Stamo Petkov
    /// Created: 07.05.2016
    /// Description: Data to prepare for sending to Borica
    /// </summary>
    public class BoricaRequestModel
    {
        // Format of the BOReq message in version 1.1
        // No.	Field name								Format
        // 1	Transaction type						N[2]
        // 2	Date/Time								YYYYMMDDHHMISS
        // 3	Transaction amount						N[12]
        // 4	Terminal identifier						A[8]
        // 5	Number of the order at the merchant		A[15]
        // 6	Brief description of the deal			A[125]
        // 7	Language								А[2]
        // 8	Protocol version						A[3]
        // 9	Currency								A[3]
        // 10	Digital signature						B[128]

        private BoricaTransactionType transactionType;

        private string orderNumber;

        private int amount;

        private string description;

        private string lang;

        private string currency;

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
        /// Brief description of the transaction.
        /// Maximum 125 symbols in range ASCII code 33 - 126.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                StringValidator(value, 125, "Description");
                this.description = String.Format("{0,-125}", value);
            }
        }

        /// <summary>
        /// Prefered interface language. 
        /// Possible values: BG, EN
        /// </summary>
        public string Language
        {
            get
            {
                return this.lang;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The property \"Language\" can not be null!");
                }

                if (!BoricaConstants.allowedLanguages.Contains(value.ToUpper()))
                {
                    throw new ArgumentOutOfRangeException("Unsupported language! Only {0} are supported!", 
                        String.Join(", ", BoricaConstants.allowedLanguages));
                }

                this.lang = value.ToUpper();
            }
        }

        /// <summary>
        /// Transaction currency.
        /// Possible values: BGN, USD, EUR
        /// </summary>
        public string Currency
        {
            get
            {
                return this.currency;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The property \"Currency\" can not be null!");
                }

                if (!BoricaConstants.allowedCurrencies.Contains(value.ToUpper()))
                {
                    throw new ArgumentOutOfRangeException("Unsupported currency! Only {0} are supported!",
                        String.Join(", ", BoricaConstants.allowedCurrencies));
                }

                this.currency = value.ToUpper();
            }
        }

        /// <summary>
        /// Validates string properties
        /// </summary>
        /// <param name="value">Property value</param>
        /// <param name="maxLength">Maximum length of the property</param>
        /// <param name="propertyName">Name of the property</param>
        /// <returns></returns>
        private bool StringValidator(string value, int maxLength, string propertyName)
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
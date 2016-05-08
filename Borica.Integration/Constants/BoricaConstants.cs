using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Borica.Integration.Constants
{
    /// <summary>
    /// Author: Stamo Petkov
    /// Created: 07.05.2016
    /// Description: Constants to use in project
    /// </summary>
    internal static class BoricaConstants
    {
        /// <summary>
        /// Normally executed authorization
        /// </summary>
        public const string Success = "00";

        /// <summary>
        /// Error messages
        /// </summary>
        private static Dictionary<string, string> ErrorMessage = new Dictionary<string, string>()
            {
                { "85", "Reversal transaction with these parameters has already been registered by the system"},
                { "86", "A transaction with these parameters has already been registered by the system"},
                { "87", "Wrong protocol version"},
                { "88", "For managing transactions. No BOReq parameter has been submitted"},
                { "89", "For managing transactions. Initial transaction not found. (Example: in reversal – the initial transaction on which reversal should be performed has not been found)"},
                { "90", "The card is not registered with the Directory server"},
                { "91", "Authorization system timeout"},
                { "92", "During ‘Check of the status of a transaction’ operation. The sent eBorica parameter is in invalid format."},
                { "93", "Unsuccessful 3 D authentication by the ACS"},
                { "94", "Cancelled transaction"},
                { "95", "Invalid merchant signature"},
                { "96", "Technical error during transaction processing"},
                { "97", "Reversal rejected"},
                { "98", "During ‘Check of the status of a transaction’ operation. For this BOReq, no registration of a BOResp is registered on the BORICA-BANKSERVICE site"},
                { "99", "Authorization rejected by the TPSS"}
            };

        /// <summary>
        /// Allowed Borica interface languages
        /// </summary>
        public static string[] allowedLanguages = new string[] { "BG", "EN" };

        /// <summary>
        /// Allowed curencies in Borica sysytem
        /// </summary>
        public static string[] allowedCurrencies = new string[] { "BGN", "EUR", "USD" };

        /// <summary>
        /// Get ErrorMessage by code
        /// </summary>
        /// <param name="code">Error code</param>
        /// <returns></returns>
        public static string GetErrorMessage(string code)
        {
            if (code == Success)
            {
                return "Normally executed authorization";
            }

            if (ErrorMessage.ContainsKey(code))
            {
                return ErrorMessage[code];
            }

            return "Unknown error!";
        }
    }
}

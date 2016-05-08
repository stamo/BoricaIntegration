using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Borica.Integration.Constants;
using Borica.Integration.Enumerations;
using Borica.Integration.Helper;
using Borica.Integration.Models;

namespace Borica.Integration
{
    /// <summary>
    /// Author: Stamo Petkov
    /// Created: 07.05.2016
    /// Description: Integration with Borica payment operator
    /// </summary>
    public class Borica
    {
        /// <summary>
        /// Virtual POS Identificator
        /// </summary>
        private string terminalId;

        /// <summary>
        /// Path to the private key file
        /// </summary>
        private string privateKeyPath;

        /// <summary>
        /// Path to the Borica public key file
        /// </summary>
        private string publicKeyPath;

        /// <summary>
        /// Empty constructor to use with IoC containers. 
        /// You must run Borica.Configure method.
        /// </summary>
        public Borica()
        {
            
        }

        /// <summary>
        /// Creates and configures Borica service
        /// </summary>
        /// <param name="terminalId">Virtual POS Identificator</param>
        /// <param name="privateKeyPath">Path to the private key file</param>
        /// <param name="publickKeyPath">Path to the Borica public key file</param>
        public Borica(string terminalId, string privateKeyPath, string publickKeyPath)
        {
            this.terminalId = terminalId;
            this.privateKeyPath = privateKeyPath;
            this.publicKeyPath = publickKeyPath;
        }

        /// <summary>
        /// Sets Borica service configuration parameters.
        /// </summary>
        /// <param name="terminalId">Virtual POS Identificator</param>
        /// <param name="privateKeyPath">Path to the private key file</param>
        public void Configure(string terminalId, string privateKeyPath, string publickKeyPath)
        {
            this.terminalId = terminalId;
            this.privateKeyPath = privateKeyPath;
            this.publicKeyPath = publickKeyPath;
        }

        /// <summary>
        /// Encodes and signs parameters for Borica payment request
        /// </summary>
        /// <param name="model">Request parameters</param>
        /// <returns>String value to be set to eBorica request parameter</returns>
        public string GetRequestParameter(BoricaRequestModel model)
        {
            string message = String.Format("{0}{1}{2:D12}{3}{4}{5}{6}1.1{7}",
                model.TransactionType,
                DateTime.Now.ToString(BoricaConstants.BoricaDateTime),
                model.Amount,
                model.TerminalId,
                model.OrderNumber,
                model.Description,
                model.Language,
                model.Currency);

            return SignMessage(message);
        }

        /// <summary>
        /// Verifies and parses Borica responce
        /// </summary>
        /// <param name="eBorica">Value of eBorica responce parameter</param>
        /// <returns></returns>
        public BoricaResponceModel ParseResponce(string eBorica)
        {
            return new BoricaResponceModel();
        }

        /// <summary>
        /// Encodes and signs parameters for 
        /// Borica lost transaction request
        /// </summary>
        /// <param name="model">Lost Transaction request parameters</param>
        /// <returns></returns>
        public BoricaTransactionStatusModel GetTransactionStatusRequest(BoricaTSRequestModel model)
        {
            return new BoricaTransactionStatusModel();
        }

        /// <summary>
        /// Signes message, using provided private key
        /// </summary>
        /// <param name="message">Message to sign</param>
        /// <returns>URL encoded Base64 string ready to send to Borica</returns>
        private string SignMessage(string message)
        {
            try
            {
                string keyString = null;

                if (!File.Exists(this.privateKeyPath))
                {
                    throw new FileNotFoundException("The path to private key is invalid!");
                }

                using (TextReader tr = new StreamReader(this.privateKeyPath))
                {
                    keyString = tr.ReadToEnd();
                }

                byte[] keyBytes = CryptoHelper.GetBytesFromPEM(keyString, PemStringType.RsaPrivateKey);
                var signedMessage = SignHelper.SignData(message, keyBytes);

                var encoder = new ASCIIEncoding();

                return HttpUtility.UrlEncode(Convert.ToBase64String(encoder.GetBytes(message).Concat(signedMessage).ToArray()));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error during signing of the transaction! Please, check inner exception for details.", ex);
            }
        }
    }
}

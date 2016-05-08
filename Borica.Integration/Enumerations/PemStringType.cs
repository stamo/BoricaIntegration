using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Borica.Integration.Enumerations
{
    /// <summary>
    /// Enumeration of RSA key types
    /// </summary>
    public enum PemStringType
    {
        /// <summary>
        /// X509Certificate
        /// </summary>
        Certificate,

        /// <summary>
        /// RSA Private Key
        /// </summary>
        RsaPrivateKey,

        /// <summary>
        /// RSA Public key
        /// </summary>
        RsaPublicKey
    }
}

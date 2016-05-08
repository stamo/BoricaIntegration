
namespace Borica.Integration.Enumerations
{
    /// <summary>
    /// Author: Stamo Petkov
    /// Created: 03.05.2016
    /// Description: 
    /// </summary>
    public enum BoricaTransactionType
    {
        Authorization = 10,
        DeferredAuthorization = 21,
        DeferredAuthorizationPerformance = 22,
        DeferredAuthorizationReversal = 23,
        SubscriptionPayment = 31,
        SubscriptionPaymentAuthorization = 32,
        SubscriptionPaymentReversal = 33,
        SubscriptionPaymentClosing = 34,
        Reversal = 40
    }
}

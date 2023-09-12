using GHSpar.Models;

namespace GHSpar.Abstractions
{
    public interface IMomoHelper
    {
        Task<PurchaseResponse> SendPurchaseRequest(PurchaseRequest purchaseRequest);
    }
}
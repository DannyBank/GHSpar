using GHSpar.Models.Db;

namespace GHSpar.Abstractions
{
    public interface IMomoHelper
    {
        Task<PurchaseResponse> SendPurchaseRequest(PurchaseRequest purchaseRequest);
    }
}
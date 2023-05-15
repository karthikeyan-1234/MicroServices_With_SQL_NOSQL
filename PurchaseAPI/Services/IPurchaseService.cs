using ERPModels;

namespace PurchaseAPI.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseDTO> AddPurchaseAsync(PurchaseDTO newPurchase);
        Task<PurchaseDetailDTO> AddPurchaseDetail(PurchaseDetailDTO newPurchaseDetail);
        Task<PurchaseDTO> GetPurchaseByID(int purchase_id);
    }
}
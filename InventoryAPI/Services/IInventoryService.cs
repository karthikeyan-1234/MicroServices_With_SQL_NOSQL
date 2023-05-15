using ERPModels;

namespace InventoryAPI.Services
{
    public interface IInventoryService
    {
        Task<InventoryDTO> AddNewInventory(InventoryDTO newInventory);
        Task<InventoryDTO> GetInventoryByItem(int item_id);
        Task<InventoryDTO> UpdateInventory(InventoryDTO revisedInventory);
    }
}
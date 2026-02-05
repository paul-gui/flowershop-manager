using FlowerShopAPI.Contracts.Warehouses;

namespace FlowerShopAPI.Managers.Warehouses.Contract
{
    public interface IWarehousesManager
    {
        Task<List<WarehouseResponse>> GetWarehouses();
        Task<WarehouseDetailsResponse?> GetWarehouseById(Guid id);
        Task<WarehouseDetailsResponse> CreateWarehouse(CreateWarehouseRequest createWarehouseRequest);
        Task<WarehouseDetailsResponse?> UpdateWarehouse(UpdateWarehouseRequest updateWarehouseRequest);
        Task<WarehouseDetailsResponse?> DeleteWarehouse(Guid id);
    }
}

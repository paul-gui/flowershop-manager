using AutoMapper;
using FlowerShopAPI.Contracts.Warehouses;
using FlowerShopAPI.Data;
using FlowerShopAPI.Managers.Warehouses.Contract;
using Microsoft.EntityFrameworkCore;

namespace FlowerShopAPI.Managers.Warehouses
{
    public class WarehousesManager(ApplicationDbContext context, IMapper mapper) : IWarehousesManager
    {
        public async Task<WarehouseDetailsResponse> CreateWarehouse(CreateWarehouseRequest createWarehouseRequest)
        {
            var warehouse = mapper.Map<Models.Warehouse>(createWarehouseRequest);
            context.Warehouses.Add(warehouse);
            await context.SaveChangesAsync();

            var result = mapper.Map<WarehouseDetailsResponse>(warehouse);

            return result;
        }

        public async Task<WarehouseDetailsResponse?> GetWarehouseById(Guid id)
        {
            var warehouse = await context.Warehouses.Where(w => w.IsActive).Include(w => w.Products.Where(p => p.IsActive)).ThenInclude(p => p.Prices).ThenInclude(p => p.Destination).FirstOrDefaultAsync(w => w.Id == id);
            var result = mapper.Map<WarehouseDetailsResponse>(warehouse);

            return result;
        }

        public async Task<List<WarehouseResponse>> GetWarehouses()
        {
            var warehouses = await context.Warehouses.Where(w => w.IsActive).ToListAsync();
            var result = mapper.Map<List<WarehouseResponse>>(warehouses);

            return result;
        }

        public async Task<WarehouseDetailsResponse?> UpdateWarehouse(UpdateWarehouseRequest updateWarehouseRequest)
        {
            var warehouse = await context.Warehouses.FindAsync(updateWarehouseRequest.Id);

            if (warehouse is null || !warehouse.IsActive)
            {
                return null;
            }

            warehouse.Name = updateWarehouseRequest.Name;
            
            await context.SaveChangesAsync();

            var result = mapper.Map<WarehouseDetailsResponse>(warehouse);

            return result;
        }

        public async Task<WarehouseDetailsResponse?> DeleteWarehouse(Guid id)
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(w => w.Id == id);

            if(warehouse == null)
            {
                return null;
            }

            warehouse.IsActive = false;
            await context.SaveChangesAsync();

            var result = mapper.Map<WarehouseDetailsResponse>(warehouse);
            return result;
        }
    }
}

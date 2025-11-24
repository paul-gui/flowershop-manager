using AutoMapper;
using FlowershopAPI.Contracts.Warehouses;
using FlowershopAPI.Data;
using FlowershopAPI.Managers.Warehouses.Contract;
using Microsoft.EntityFrameworkCore;

namespace FlowershopAPI.Managers.Warehouses
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
            var warehouse = await context.Warehouses.Include(w => w.Products).FirstOrDefaultAsync(w => w.Id == id);
            var result = mapper.Map<WarehouseDetailsResponse>(warehouse);

            return result;
        }

        public async Task<List<WarehouseResponse>> GetWarehouses()
        {
            var warehouses = await context.Warehouses.ToListAsync();
            var result = mapper.Map<List<WarehouseResponse>>(warehouses);

            return result;
        }

        public async Task<WarehouseDetailsResponse?> UpdateWarehouse(UpdateWarehouseRequest updateWarehouseRequest)
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(w => w.Id == updateWarehouseRequest.Id);

            if (warehouse == null)
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

            context.Warehouses.Remove(warehouse);
            await context.SaveChangesAsync();

            var result = mapper.Map<WarehouseDetailsResponse>(warehouse);
            return result;
        }
    }
}

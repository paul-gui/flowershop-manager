import api from "@/services/API";
import type {
  CreateWarehouseRequest,
  UpdateWarehouseRequest,
} from '@/types/dtos/warehouse/warehouseRequests.dto.ts'
import type { WarehouseDetailsResponse, WarehouseResponse } from '@/types/dtos/warehouse/warehouseResponses.dto.ts'

const baseUrl = 'Warehouses'

export async function createWarehouse(createWarehouseRequest: CreateWarehouseRequest):Promise<WarehouseDetailsResponse>{
    const res = await api.post(baseUrl + '/CreateWarehouse', createWarehouseRequest)
    return res.data
}

export async function getWarehouses():Promise<WarehouseResponse[]>{
    const res = await api.get(baseUrl + '/GetWarehouses');
    return res.data
}

export async function getWarehouse(id: string):Promise<WarehouseDetailsResponse>{
    const res = await api.get(baseUrl + '/GetWarehouse/' + id);
    return res.data
}

export async function updateWarehouse(updateWarehouseRequest: UpdateWarehouseRequest):Promise<WarehouseDetailsResponse>{
    const res = await api.put(baseUrl + '/UpdateWarehouse/', updateWarehouseRequest)
    return res.data
}

export async function deleteWarehouse(id: string):Promise<WarehouseDetailsResponse>{
    const res = await api.delete(baseUrl + '/DeleteWarehouse/' + id);
    return res.data
}
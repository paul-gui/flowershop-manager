import api from "@/services/API";
import type { WarehouseRequest } from '@/types/dtos/warehouse/warehouse.dto.ts'

const baseUrl = 'Warehouses'

export async function addWarehouse(warehouse: WarehouseRequest){
    const res = await api.post(baseUrl + '/CreateWarehouse', warehouse)
    return res.data
}

export async function getWarehouses(){
    const res = await api.get(baseUrl + '/GetWarehouses');
    return res.data
}

export async function getWarehouse(id: string){
    const res = await api.get(baseUrl + '/GetWarehouse/' + id);
    return res.data
}

export async function editWarehouse(id: string, warehouse: WarehouseRequest){
    const res = await api.put(baseUrl + '/EditWarehouse/' + id, warehouse)
    return res.data
}

export async function deleteWarehouse(id: string){
    const res = await api.delete(baseUrl + '/DeleteWarehouse/' + id);
    return res.data
}
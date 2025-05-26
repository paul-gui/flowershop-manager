import api from "@/services/API";
import { WarehouseDto } from "@/dtos/dataInputDTOs";

const baseUrl = 'Warehouses'

export async function addWarehouse(warehouse: WarehouseDto){
    const res = await api.post(baseUrl + '/CreateWarehouse', warehouse)
    return res.data
}

export async function getWarehouses(){
    const res = await api.get(baseUrl + '/GetWarehouses');
    return res.data
}
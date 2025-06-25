import api from '@/services/API';
import type { ProductDto } from "@/dtos/dataInputDTOs.ts";

const baseUrl = '/Products'

export async function getProducts(warehouseId: string){
    const res = await api.get(baseUrl + '/GetProducts/' + warehouseId);
    return res.data
}

export async function addProduct(product: ProductDto){
    const res = await api.post(baseUrl + '/AddProduct', product);
    return res.data
}

export async function deleteProduct(id: string){
    const res = await api.delete(baseUrl + '/DeleteProduct/' + id);
    return res.data
}
import api from '@/services/API';
import type {
  CreateProductRequest,
  UpdateProductRequest,
} from '@/types/dtos/products/productRequests.dto.ts'
import type { ProductResponse } from '@/types/dtos/products/productResponses.dto.ts'
import type { List } from 'postcss/lib/list'
import type { DestinationResponse } from '@/types/dtos/destinations/destinationResponses.dto.ts'

const baseUrl = '/Products'

export async function getProducts(warehouseId: string){
    const res = await api.get(baseUrl + '/GetProducts/' + warehouseId);
    return res.data
}

export async function createProduct(product: CreateProductRequest):Promise<String>{
  const res = await api.post(baseUrl + '/CreateProduct', product);
  return res.data
}

export async function getProductById(id: string):Promise<ProductResponse>{
  const res = await api.get(baseUrl + '/GetProduct/' + id);
  return res.data
}

export async function getProductsByWarehouseId(warehouseId: string):Promise<ProductResponse[]>{
  const res = await api.get(baseUrl + '/GetProductsByWarehouseId/' + warehouseId);
  return res.data
}

export async function getPrice(productId: string, destinationId: string):Promise<number>{
  const res = await api.get(baseUrl + '/GetPrice/' + productId + '/' + destinationId);
  return res.data
}

export async function updateProduct(product: UpdateProductRequest):Promise<ProductResponse>{
  const res = await api.put(baseUrl + '/UpdateProduct/', product);
  return res.data
}

export async function deleteProduct(id: string):Promise<ProductResponse>{
    const res = await api.delete(baseUrl + '/DeleteProduct/' + id);
    return res.data
}

export async function getDestinations():Promise<DestinationResponse[]>{
  const res = await api.get(baseUrl + '/GetDestinations');
  return res.data
}
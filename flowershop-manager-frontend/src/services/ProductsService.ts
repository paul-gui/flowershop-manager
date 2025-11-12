import api from '@/services/API';
import type {
  CreateProductRequest,
  UpdateProductRequest,
} from '@/types/dtos/products/product.dto.ts'

const baseUrl = '/Products'

export async function getProducts(warehouseId: string){
    const res = await api.get(baseUrl + '/GetProducts/' + warehouseId);
    return res.data
}

export async function addProduct(product: CreateProductRequest){
  try{
    const res = await api.post(baseUrl + '/AddProduct', product);
    return res.data
  }
  catch (error){
    console.log('Error adding product:' + error);
    throw error;
  }
}

export async function getProductById(id: string){
  const res = await api.get(baseUrl + '/GetProduct/' + id);
  return res.data
}

export async function updateProduct(product: UpdateProductRequest){
  const res = await api.put(baseUrl + '/UpdateProduct/', product);
  return res.data
}

export async function deleteProduct(id: string){
    const res = await api.delete(baseUrl + '/DeleteProduct/' + id);
    return res.data
}

export async function getDestinations(){
  const res = await api.get(baseUrl + '/GetDestinations');
  return res.data
}
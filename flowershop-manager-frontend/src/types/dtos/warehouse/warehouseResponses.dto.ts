import type { ProductResponse } from '@/types/dtos/products/productResponses.dto.ts'

export interface WarehouseResponse {
  id: string;
  name: string;
}

export interface WarehouseDetailsResponse {
  id: string;
  name: string;
  products: ProductResponse[]
}
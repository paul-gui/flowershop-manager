export interface ProductResponse {
  id: string;
  name: string;
  warehouseId: string;
  prices: Array<PriceForProductResponse>;
}

export interface PriceForProductResponse {
  destinationId: string;
  destinationName: string;
  value: number;
}
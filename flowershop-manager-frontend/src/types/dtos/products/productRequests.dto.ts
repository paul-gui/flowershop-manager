export interface CreateProductRequest {
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<PriceForProductRequest>
}

export interface UpdateProductRequest {
  id: string;
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<PriceForProductRequest>;
}

export interface PriceForProductRequest {
  destinationId: string;
  value: number;
}
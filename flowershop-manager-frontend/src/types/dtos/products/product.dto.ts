export interface CreateProductRequest {
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<ProductPriceForDestinationWithName>
}

export interface ProductPriceForDestinationWithName {
  destinationId: string;
  destinationName: string;
  value: number;
}

export interface UpdateProductRequest {
  id: string;
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<ProductPriceForDestinationWithName>;
}

export interface CreateProductResponse {
  productId: string;
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<ProductPriceForDestinationWithName>;
}
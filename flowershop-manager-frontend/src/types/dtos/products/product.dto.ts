export interface CreateProductRequest {
  name: string;
  warehouseId: string;
  prices: ReadOnlyArray<ProductPriceForDestinationWithName>
}

export interface ProductPriceForDestination{
  destinationId: string;
  value: number;
}

export interface ProductPriceForDestinationWithName{
  destinationId: string;
  destinationName: string;
  value: number;
}

export interface CreateProductResponse {
  productId: string;
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<ProductPriceForDestinationWithName>;
}
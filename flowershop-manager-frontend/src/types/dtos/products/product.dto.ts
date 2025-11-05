export interface CreateProductRequest {
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<ProductPriceForDestination>
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
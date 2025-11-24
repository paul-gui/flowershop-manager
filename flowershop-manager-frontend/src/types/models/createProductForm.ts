export interface CreateProductForm {
  name: string;
  warehouseId: string;
  prices: ReadonlyArray<PriceForProductCreation>
}

export interface PriceForProductCreation{
  destinationId: string;
  destinationName: string;
  value: number;
}
export interface CreateProductForm {
  name: string;
  warehouseId: string;
  prices: Array<PriceForProductCreation>
}

export interface PriceForProductCreation{
  destinationId: string;
  destinationName: string;
  value: number;
}
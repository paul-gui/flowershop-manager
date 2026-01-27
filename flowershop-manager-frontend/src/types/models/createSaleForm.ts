export interface CreateSaleForm {
  productId: string,
  destinationId: string,
  quantity: number,
  priceAtSale: number,
  saleDate: String
}

export interface WarehouseOption {
  id: string | null,
  name: string,
}

export interface DestinationOption {
  id: string | null,
  name: string,
}
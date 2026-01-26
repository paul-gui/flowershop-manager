export interface SaleResponse {
  id: string,
  productName: string,
  warehouseName: string,
  destinationName: string,
  quantity: number,
  priceAtSale: number,
  saleDate: string,
}

export interface SaleResponseForEdit {
  id: string,
  productName: string,
  productId: string,
  warehouseName: string,
  destinationId: string,
  quantity: number,
  priceAtSale: number,
  saleDate: string,
}
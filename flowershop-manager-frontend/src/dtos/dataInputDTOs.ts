export interface WarehouseDto {
    name: string;
}
export interface ProductDto{
    name: string;
    warehouseId: string;
    prices:{
        destinationId: string;
        value: number;
    }[]
}
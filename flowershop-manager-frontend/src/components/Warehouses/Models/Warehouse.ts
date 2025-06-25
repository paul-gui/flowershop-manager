export interface Warehouse {
    id: string;
    name: string;
    products: Product[];
}

export interface Product {
    id: string;
    name: string;
    warehouseId: string;
}
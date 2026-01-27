export interface SalesFilterForm {
  warehouseId: string | null;
  destinationId: string | null;
  startDate: string;
  endDate: string;
}

export interface WarehouseOption {
  id: string | null,
  name: string,
}

export interface DestinationOption {
  id: string | null,
  name: string,
}
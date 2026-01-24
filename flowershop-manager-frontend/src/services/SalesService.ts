import type { CreateSaleForm } from '@/types/models/createSaleForm.ts'
import api from '@/services/API.ts'
import type { SalesFilterForm } from '@/types/models/salesFilterForm.ts'

const baseUrl = '/Sales'

export async function createSale(createSaleForm: CreateSaleForm) {
  const res = await api.post(baseUrl + '/CreateSale', createSaleForm)
  return res.data
}

export async function getSales(filters: SalesFilterForm) {
  const params = {
    warehouseId: filters.warehouseId,
    destinationId: filters.destinationId,
    startDate: filters.startDate,
    endDate: filters.endDate
  }
  const res = await api.get(baseUrl + '/GetSales', { params })
  return res.data
}
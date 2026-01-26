import type { CreateSaleForm } from '@/types/models/createSaleForm.ts'
import api from '@/services/API.ts'
import type { SalesFilterForm } from '@/types/models/salesFilterForm.ts'
import type { SaleEditRequest } from '@/types/dtos/sales/saleRequests.ts'

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

export async function getSaleForEdit(saleId: string) {
  const res = await api.get(baseUrl + '/GetSaleForEdit/' + saleId)
  return res.data
}

export async function updateSale(saleId: string, updateSaleForm: SaleEditRequest) {
  const params = {
    saleId: saleId
  }
  const res = await api.put(baseUrl + '/UpdateSale/' + saleId, updateSaleForm)
  return res.data
}

export async function deleteSale(saleId: string) {
  const res = await api.delete(baseUrl + '/DeleteSale/' + saleId)
  return res.data
}
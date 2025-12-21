import type { CreateSaleForm } from '@/types/models/createSaleForm.ts'
import api from '@/services/API.ts'

const baseUrl = '/Sales'

export async function createSale(createSaleForm: CreateSaleForm) {
  const res = await api.post(baseUrl + '/CreateSale', createSaleForm)
  return res.data
}
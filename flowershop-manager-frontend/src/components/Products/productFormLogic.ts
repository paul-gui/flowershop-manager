import type { CreateProductRequest, UpdateProductRequest } from '@/types/dtos/products/productRequests.dto.ts'
import type { CreateProductForm, PriceForProductCreation } from '@/types/models/createProductForm.ts'
import type { DestinationResponse } from '@/types/dtos/destinations/destinationResponses.dto.ts'

import { computed, ref } from 'vue'
import {
  createProduct,
  getDestinations,
  getProductById,
  updateProduct,
} from '@/services/ProductsService.ts'

export function useProductFormLogic(params: { warehouseId?: string, productId?: string }) {
  const isCreate = computed(() => !!params.warehouseId && !params.productId)
  const isEdit = computed(() => !!params.productId)

  const form = ref<CreateProductForm>({name: '', warehouseId:'', prices:[]})
  const destinations = ref<DestinationResponse[]>([])
  const loading = ref<boolean>(false);
  const error = ref<string | null>(null);


  async function hydrate(){
    loading.value = true
    error.value = null;
    try{
      if(isCreate.value){
        form.value.warehouseId = params.warehouseId!
        destinations.value = await getDestinations()
        form.value.prices = destinations.value.map(d => ({destinationId: d.id, destinationName: d.name, value: 0}))
      }
      else if(isEdit.value){
        const p = await getProductById(params.productId!)
        form.value.name = p.name
        form.value.warehouseId = p.warehouseId!
        form.value.prices = p.prices
          .sort((a:PriceForProductCreation, b:PriceForProductCreation) => b.destinationName.localeCompare(a.destinationName))
      }
      else {
        error.value = 'Invalid navigation'
      }
    }
    catch(e: any){
      error.value = e?.message ?? 'Failed to load'
    }
    finally{
      loading.value = false
    }
  }

  async function submitProduct(){
    if(isCreate.value){
      const createProductRequest: CreateProductRequest = {
        name: form.value.name,
        warehouseId: form.value.warehouseId,
        prices: form.value.prices.map(p => ({ destinationId: p.destinationId, value: p.value }))
      }

      await createProduct(createProductRequest)
    }
    else {
      const updateProductRequest: UpdateProductRequest = {
        id: params.productId!,
        name: form.value.name,
        warehouseId: form.value.warehouseId,
        prices: form.value.prices,
      }

      await updateProduct(updateProductRequest)
    }
  }

  return { isCreate, isEdit, form, loading, error, hydrate, submitProduct }
}
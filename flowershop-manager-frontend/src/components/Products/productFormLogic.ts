import type {
  CreateProductRequest,
  ProductPriceForDestinationWithName,
  UpdateProductRequest,
} from '@/types/dtos/products/product.dto.ts'

import { computed, ref } from 'vue'
import {
  addProduct,
  getDestinations,
  getProductById,
  updateProduct,
} from '@/services/ProductsService.ts'
import type { Destination } from '@/types/models/destination.ts'

export function useProductFormLogic(params: { warehouseId?: string, productId?: string }) {
  const isCreate = computed(() => !!params.warehouseId && !params.productId)
  const isEdit = computed(() => !!params.productId)

  const form = ref<CreateProductRequest>({name: '', warehouseId:'', prices:[]})
  const destinations = ref<Destination[]>([])
  const loading = ref<boolean>(false);
  const error = ref<string | null>(null);

  async function loadDestinations(){
    return await getDestinations()
  }

  async function getProduct(id: string){
    return await getProductById(id)
  }

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
          .sort((a:ProductPriceForDestinationWithName, b:ProductPriceForDestinationWithName) => b.destinationName.localeCompare(a.destinationName))
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
      await addProduct(form.value)
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

  return { isCreate, isEdit, form, loading, error, destinations, hydrate, submitProduct }
}
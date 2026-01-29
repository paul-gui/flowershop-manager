<template>
  <div class="h-full bg-background text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto">
      <h1 class="text-h1 text-text_primary mb-4">Adaugare vanzare</h1>
      <div v-if="isLoading" class="flex justify-center items-center py-10 gap-2">
        <div class="h-10 w-10 animate-spin rounded-full border-4 border-gray-300 border-t-gray-600"></div>
      </div>
      <div v-else>
        <div class="mb-4">
          <label class="block text-sm mb-2">Produs</label>
          <div
            v-if="products.length === 0"
            class="flex items-center justify-center p-4">
            <p>Nu exista produse</p>
          </div>
          <div
            v-else
            class="space-y-1 max-h-44 overflow-scroll">
            <div
              :class="[
            'p-3 rounded-lg cursor-pointer transition duration-150 ease-in-out',
            createSaleForm?.productId === p.id
              ? 'bg-accent3 text-white shadow-lg'
              : 'bg-cards text-gray-300 hover:bg-divider',
          ]"
              @click="selectProduct(p)"
              v-for="p in products"
            >
              {{ p.name }}
            </div>
          </div>
          <p class="text-red-500 text-sm" v-if="errors['product']">{{ errors['product'] }}</p>
        </div>

        <div class="mb-4">
          <label class="block text-sm mb-2">Destinație</label>
          <div class="flex rounded-lg overflow-hidden bg-cards p-1">
            <button
              :class="[
            'flex-1 py-3 font-medium transition duration-150 ease-in-out rounded-lg',
            createSaleForm.destinationId === d.id
              ? 'bg-accent3 text-white shadow-md'
              : 'text-gray-300 hover:bg-divider',
          ]"
              @click="selectDestination(d)"
              v-for="d of destinations"
            >
              {{ d.name }}
            </button>
          </div>
          <p class="text-red-500 text-sm" v-if="errors['destination']">{{ errors['destination'] }}</p>
        </div>

        <div class="mb-4">
          <label class="block text-sm mb-2">Cantitate</label>
          <input
            v-model.number="createSaleForm.quantity"
            type="number"
            class="w-full p-3 text-xl font-bold text-center bg-cards text-white rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 transition"
          />
          <p class="text-red-500 text-sm" v-if="errors['quantity']">{{ errors['quantity'] }}</p>
        </div>

        <div class="mb-6">
          <label class="block text-sm mb-2">Pret</label>
          <input
            v-model.number="createSaleForm.priceAtSale"
            type="number"
            class="w-full p-3 text-xl font-bold text-center bg-cards text-white rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 transition"
          >
          <p class="text-red-500 text-sm" v-if="errors['price']">{{ errors['price'] }}</p>
        </div>
      </div>

      <div class="sticky bottom-0 flex justify-center mt-6 gap-2 w-full bg-background pt-1">
        <button
          @click="goBack"
          class="bg-cards hover:bg-[#3c3860] text-gray-50 py-3 px-10 rounded-xl"
        >
          Anuleaza
        </button>
        <button
          @click="submitForm"
          class="bg-accent2 hover:bg-green-600 text-text_accents py-3 px-10 rounded-xl"
        >
          Salveaza
        </button>
      </div>
    </div>
  </div>

</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getDestinations, getPrice, getProductsByWarehouseId } from '@/services/ProductsService.ts'
import type { ProductResponse } from '@/types/dtos/products/productResponses.dto.ts'
import type { CreateSaleForm } from '@/types/models/createSaleForm.ts'
import type { DestinationResponse } from '@/types/dtos/destinations/destinationResponses.dto.ts'
import { useRoute } from 'vue-router'
import { createSale } from '@/services/SalesService.ts'
import router from '@/router'
import { toast } from 'vue-sonner'

const route = useRoute();

const createSaleForm = ref<CreateSaleForm>({
  productId: '',
  destinationId: '',
  quantity: 0,
  priceAtSale: 0,
  saleDate: ''
})
const products = ref<ProductResponse[]>([])
const destinations = ref<DestinationResponse[]>([])
const errors = ref<Record<string, string>>({})
const isLoading = ref<boolean>(false)

onMounted( async () => {
  const warehouseId = String(route.params.warehouseId);
  if (!warehouseId) {
    products.value = [];
    return;
  }
  try{
    isLoading.value = true
    products.value = await getProductsByWarehouseId(warehouseId)
    destinations.value = await getDestinations()
  }
  catch(error) {
    toast.error('A aparut o eroare la incarcarea formuluarului')
  }
  finally {
    isLoading.value = false
  }
});

// Methods
const selectProduct = (product: ProductResponse) => {
  createSaleForm.value.productId = product.id;
  updatePrice();
};

const selectDestination = (destination: DestinationResponse) => {
  createSaleForm.value.destinationId = destination.id;
  updatePrice();
};

const updatePrice = async () => {
  if (!createSaleForm.value.productId || !createSaleForm.value.destinationId) return;
  createSaleForm.value.priceAtSale = await getPrice(createSaleForm.value.productId, createSaleForm.value.destinationId)
};

const goBack = async () => {
  await router.replace({ name: 'Warehouses' });
}

const getTodayDate = () => {
  const today = new Date();
  const y = today.getFullYear();
  const m = String(today.getMonth() + 1).padStart(2, '0');
  const d = String(today.getDate()).padStart(2, '0');
  return `${y}-${m}-${d}`;
}

const submitForm = async () => {
  errors.value = {};
  if (createSaleForm.value.productId === '') {
    errors.value['product'] = 'Alege un produs';
  }
  if (createSaleForm.value.destinationId === '') {
    errors.value['destination'] = 'Alege o destinatie';
  }
  if (createSaleForm.value.quantity <= 0) {
    errors.value['quantity'] = 'Cantitatea trebuie sa fie mai mare de 0'
  }
  if (createSaleForm.value.priceAtSale <= 0) {
    errors.value['price'] = 'Pretul trebuie sa fie mai mare de 0'
  }
  if (Object.values(errors.value).length > 0) {
    return
  }

  createSaleForm.value.saleDate = getTodayDate()

  try {
    await createSale(createSaleForm.value)
    toast.success('Vanzare adaugata cu succes')
    await router.replace({ name: 'Warehouses' })
  }
  catch (error) {
    toast.error('A aparut o eroare')
  }
};
</script>
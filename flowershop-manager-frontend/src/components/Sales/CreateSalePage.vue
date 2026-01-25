<template>
  <div class="p-6 max-w-sm mx-auto">
    <div class="mb-6">
      <label class="block text-gray-400 text-sm mb-2">Opțiune</label>
      <div class="space-y-1">
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
    </div>
    <hr class="border-gray-800 my-6" />

    <div class="mb-6">
      <label class="block text-gray-400 text-sm mb-2">Destinație</label>
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
    </div>
    <div class="mb-6">
      <label class="block text-gray-400 text-sm mb-2">Cantitate</label>
      <input
        v-model.number="createSaleForm.quantity"
        type="number"
        class="w-full p-4 text-2xl font-bold text-center bg-cards text-white rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 transition"
      />
    </div>

    <div class="mb-8">
      <label class="block text-gray-400 text-sm mb-2">Pret</label>
      <input
        v-model.number="createSaleForm.priceAtSale"
        type="number"
        class="w-full p-4 text-2xl font-bold text-center bg-cards text-white rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 transition"
      >
    </div>

    <div>
      <label class="block text-red-600" v-if="errors" v-for="e in errors">
        {{ e }}
      </label>
    </div>

    <button
      @click="submitForm"
      class="w-full py-4 font-bold text-gray-900 bg-accent2 rounded-lg hover:bg-green-500 transition duration-150 ease-in-out shadow-lg"
    >
      Adauga
    </button>

    <button
      @click="router.back()"
      class="w-full py-4 mt-4 font-bold text-gray-900 bg-error rounded-lg hover:bg-red-500 transition duration-150 ease-in-out shadow-lg"
    >
      Anuleaza
    </button>
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
const errors = ref<string[]>([])

onMounted( async () => {
  const warehouseId = String(route.params.warehouseId);
  if (!warehouseId) {
    products.value = [];
    return;
  }
  products.value = await getProductsByWarehouseId(warehouseId)
  destinations.value = await getDestinations()
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

const getTodayDate = () => {
  const today = new Date();
  const y = today.getFullYear();
  const m = String(today.getMonth() + 1).padStart(2, '0');
  const d = String(today.getDate()).padStart(2, '0');
  return `${y}-${m}-${d}`;
}

const submitForm = async () => {
  errors.value = [];
  if (createSaleForm.value.quantity <= 0){
    errors.value.push('Cantitatea trebuie sa fie mai mare de 0');
  }
  if (createSaleForm.value.priceAtSale <= 0){
    errors.value.push('Pretul trebuie sa fie mai mare de 0');
  }
  if (errors.value.length > 0) return;

  createSaleForm.value.saleDate = getTodayDate()

  const result = createSale(createSaleForm.value)
    .then(() => {
      router.back()
    })
    .catch((err) => {
    errors.value = err.response.data;
  })
};
</script>
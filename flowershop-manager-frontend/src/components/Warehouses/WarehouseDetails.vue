<template>
  <div class="h-screen bg-background content-center text-text_primary p-4 py-12 space-y-4">

      <div class="flex justify-end">
        <button @click="showModal = true" class="bg-error hover:bg-red-600 text-text_accents py-2 px-8 rounded-xl">Sterge</button>
        <ConfirmModal
            v-model="showModal"
            title="Confirmare"
            message="Sigur doresti sa stergi?"
            @confirm="deleteWarehouseButton"
        />
      </div>
      <!-- Name Field -->
      <div>
        <label class="block text-sm mb-1">Denumire</label>
        <input
            v-model="name"
            type="text"
            class="w-full bg-cards text-xl text-text_secondary px-4 py-2 rounded-xl focus:outline-none"
        />
      </div>
      <!-- Flowers List -->
      <div>
        <label class="block text-sm mb-2">Flori</label>
        <div class="max-h-96 overflow-scroll">
          <div v-for="(flower, index) in flowers" :key="index" class="mb-2">
            <content-button :title="flower.name" description="Pret" icon="fa fa-trash text-red-500" @primary-click="() => { goToDetails(index) }" @secondary-click="() => { removeFlower(index) }"></content-button>
          </div>
        </div>

        <button
            v-on:click="addFlower"
            class="mt-2 flex items-center gap-2 text-sm text-text_primary bg-cards px-4 py-2 rounded-xl hover:bg-[#3c3860]"
        >
          <i class="fa fa-plus" />
          Adauga
        </button>
      </div>
      <!-- Actions -->
      <div class="fixed bottom-8 left-0 flex justify-center mt-6 space-x-4 w-full">
        <button @click="goBack" class="bg-error hover:bg-red-600 text-text_accents py-3 px-10 rounded-xl">Anuleaza</button>
        <button @click="saveWarehouse" class="bg-accent2 hover:bg-green-600 text-text_accents py-3 px-10 rounded-xl">Salveaza</button>
      </div>
    </div>

</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getWarehouse, editWarehouse, deleteWarehouse } from "@/services/WarehousesService.ts";
import { addProduct, deleteProduct } from "@/services/ProductsService.ts";
import type { Product, Warehouse } from "@/components/Warehouses/Models/Warehouse.ts";
import type { ProductDto, WarehouseDto } from "@/dtos/dataInputDTOs.ts";
import ContentButton from "@/components/Warehouses/content-button.vue";
import ConfirmModal from "@/components/ConfirmModal.vue";
import router from "@/router";

const props = defineProps<{
  id: string,
}>()

const name = ref('')
const flowers = ref<Product[]>([])
const warehouse = ref<Warehouse>()
const showModal = ref<boolean>(false);

onMounted(async () => {
  await getWarehouseDetails()
})

async function getWarehouseDetails(){
  warehouse.value = await getWarehouse(props.id)
  if (warehouse.value) {
    name.value = warehouse.value.name
    flowers.value = warehouse.value.products
  }
}

async function addFlower(){
  const product: ProductDto = {
    name: 'Muscata',
    warehouseId: props.id,
    prices: {
      destinationId: '1',
      value: 20.0,
    }
  }
  const res = await addProduct(product)
  await getWarehouseDetails()
  console.log(res)
}

async function saveWarehouse(){
  if (warehouse.value) {
    const warehouseDto: WarehouseDto = {
      name: name.value,
    }
    await editWarehouse(warehouse.value.id, warehouseDto)
    await goBack()
  }
}

async function goBack(){
  await router.push({ path: `/warehouses` })
}

async function deleteWarehouseButton(){
  if(warehouse.value){
    await deleteWarehouse(warehouse.value.id)
    await goBack()
  }
}

const removeFlower = async (index: number) => {
  const id = flowers.value[index].id
  const res = await deleteProduct(id)
  await getWarehouseDetails()
  console.log(res)
}

const goToDetails = async (index: number) => {
  await router.push({ path: `/product-details/${index}` })
}

</script>

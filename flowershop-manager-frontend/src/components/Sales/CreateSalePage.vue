<template>
  <div class="p-6 max-w-sm mx-auto bg-gray-900 rounded-xl shadow-lg">
    <div class="mb-6">
      <label class="block text-gray-400 text-sm mb-2">Opțiune</label>
      <div class="space-y-1">
        <div
          :class="[
            'p-3 rounded-lg cursor-pointer transition duration-150 ease-in-out',
            selectedOption === 'Muscate tiroleze'
              ? 'bg-purple-600 text-white shadow-lg'
              : 'bg-gray-800 text-gray-300 hover:bg-gray-700',
          ]"
          @click="selectOption('Muscate tiroleze')"
        >
          Muscate tiroleze
        </div>

        <div
          :class="[
            'p-3 rounded-lg cursor-pointer transition duration-150 ease-in-out',
            selectedOption === 'Muscate semi'
              ? 'bg-purple-600 text-white shadow-lg'
              : 'bg-gray-800 text-gray-300 hover:bg-gray-700',
          ]"
          @click="selectOption('Muscate semi')"
        >
          Muscate semi
        </div>

        <div
          :class="[
            'p-3 rounded-lg cursor-pointer transition duration-150 ease-in-out',
            selectedOption === 'Muscate tufa'
              ? 'bg-purple-600 text-white shadow-lg'
              : 'bg-gray-800 text-gray-300 hover:bg-gray-700',
          ]"
          @click="selectOption('Muscate tufa')"
        >
          Muscate tufa
        </div>

        <div
          :class="[
            'p-3 rounded-lg cursor-pointer transition duration-150 ease-in-out relative',
            selectedOption === 'Muscate caliope'
              ? 'bg-purple-600 text-white shadow-lg'
              : 'bg-gray-800 text-gray-300 hover:bg-gray-700',
          ]"
          @click="selectOption('Muscate caliope')"
        >
            Muscate caliope
        </div>
      </div>
    </div>
    <hr class="border-gray-800 my-6" />

    <div class="mb-6">
      <label class="block text-gray-400 text-sm mb-2">Destinație</label>
      <div class="flex rounded-lg overflow-hidden bg-gray-800 p-1">
        <button
          :class="[
            'flex-1 py-3 font-medium transition duration-150 ease-in-out rounded-lg',
            selectedDestination === 'Florarie'
              ? 'bg-purple-600 text-white shadow-md'
              : 'text-gray-300 hover:bg-gray-700',
          ]"
          @click="selectDestination('Florarie')"
        >
          Florarie
        </button>

        <button
          :class="[
            'flex-1 py-3 font-medium transition duration-150 ease-in-out rounded-lg',
            selectedDestination === 'En gros'
              ? 'bg-purple-600 text-white shadow-md'
              : 'text-gray-300 hover:bg-gray-700',
          ]"
          @click="selectDestination('En gros')"
        >
          En gros
        </button>
      </div>
    </div>
    <div class="mb-6">
      <label class="block text-gray-400 text-sm mb-2">Cantitate</label>
      <input
        v-model.number="quantity"
        type="number"
        class="w-full p-4 text-2xl font-bold text-center bg-gray-800 text-white rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500 transition"
        placeholder="0"
      />
    </div>

    <div class="mb-8">
      <label class="block text-gray-400 text-sm mb-2">Pret</label>
      <div
        class="w-full p-4 text-2xl font-bold text-center bg-gray-800 text-white rounded-lg"
      >
        {{ price.toFixed(2) }} Lei
      </div>
    </div>

    <button
      @click="submitForm"
      class="w-full py-4 text-lg font-bold text-gray-900 bg-green-400 rounded-lg hover:bg-green-500 transition duration-150 ease-in-out shadow-lg"
    >
      Adauga
    </button>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";

// State
const selectedOption = ref("Muscate tiroleze"); // Matches the image
const selectedDestination = ref("Florarie"); // Matches the image
const quantity = ref(20); // Matches the image
const unitPrice = 10; // Assuming a fixed unit price for display

// Computed Property for Price (just for the display)
const price = computed(() => {
  // In a real app, this would be more complex based on selection/destination
  return unitPrice; // We are mimicking the static "10 Lei" display in the image
});

// Methods
const selectOption = (option) => {
  selectedOption.value = option;
  // console.log("Selected Option:", option);
};

const selectDestination = (destination) => {
  selectedDestination.value = destination;
  // console.log("Selected Destination:", destination);
};

const submitForm = () => {
  const formData = {
    option: selectedOption.value,
    destination: selectedDestination.value,
    quantity: quantity.value,
    price: price.value, // This is the displayed price, not total
    totalCost: quantity.value * unitPrice, // Example calculation for total
  };
  console.log("Form Data Submitted:", formData);
  alert(
    `Form Submitted! \nOption: ${formData.option}\nDestination: ${formData.destination}\nQuantity: ${formData.quantity}\nTotal Cost (example): ${formData.totalCost} Lei`
  );
  // In a real application, you would send this data to an API here
};
</script>
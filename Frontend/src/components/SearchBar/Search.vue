<!-- Search.vue -->
<template>
  <div class="w-80 flex items-center px-4 bg-slate-100 rounded-md">
    <input
      type="text"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
      placeholder="Search Title"
      class="w-full text-xs bg-transparent py-[11px] outline-none"
    />
    <Icon icon="hugeicons:search-01" width="24" height="24" class="text-slate-400 cursor-pointer hover:text-black" />
  </div>
</template>

<script setup>
import { Icon } from '@iconify/vue';

defineProps(['modelValue']);
defineEmits(['update:modelValue']);
</script>

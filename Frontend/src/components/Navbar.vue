<!-- Navbar.vue -->
<template>
  <div class="bg-white flex items-center justify-between px-6 py-2 drop-shadow">
    <h2 class="text-3xl font-medium text-black py-2">Noted</h2>
    <Search
      :modelValue="searchQuery"
      @update:modelValue="$emit('update:searchQuery', $event)"
    />
    <ProfileInfo :onLogout="logout" />
  </div>
</template>

<script setup>
import Search from './SearchBar/Search.vue';
import ProfileInfo from '../components/Cards/ProfileInfo.vue';

defineProps(['searchQuery']);
defineEmits(['update:searchQuery']);
</script>

<template>
  <Navbar v-model:searchQuery="searchQuery" />

  <!-- Sort -->
  <div class="container mx-auto mt-4">
    <div class="flex justify-end mb-4">
      <select
        v-model="sortOption"
        class="border px-3 py-1 rounded bg-white text-sm text-slate-700"
      >
        <option value="latest">Sort: Latest</option>
        <option value="oldest">Sort: Oldest</option>
        <option value="title">Sort: A-Z</option>
      </select>
    </div>

    <!-- Notes  -->
    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
      <NotedCard
        v-for="note in filteredNotes"
        :key="note.id"
        :note="note"
        @onEdit="() => handleEdit(note)"
        @onDelete="() => noteStore.removeNote(note.id)"
      />
    </div>
  </div>

  <!-- Add Button -->
  <button
    class="h-16 w-16 flex items-center justify-center rounded-2xl bg-primary hover:bg-blue-600 fixed right-10 bottom-10 shadow-lg"
    @click="openAddModal"
    aria-label="Add Note"
  >
    <Icon icon="mdi:add" class="text-[32px] text-white" />
  </button>

  <!-- Modal -->
  <VueFinalModal
    v-model="isModalOpen"
    :content-class="modalContentClasses"
    :overlay-style="overlayStyle"
    :overlay-transition="'vfm-fade'"
    :content-transition="'vfm-slide-up'"
    lock-scroll
  >
    <div class="relative">
      <button @click="isModalOpen = false" class="absolute top-3 right-3">
        <Icon
          icon="solar:close-square-line-duotone"
          width="30"
          height="30"
          class="cursor-pointer text-red-500"
        />
      </button>

      <AddEditeNotes :note="selectedNote" @close="isModalOpen = false" />
    </div>
  </VueFinalModal>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { Icon } from '@iconify/vue';
import { useNoteStore } from '../../Stores/Note.store';
import NotedCard from '../../components/Cards/NotedCard.vue';
import Navbar from '../../components/Navbar.vue';
import AddEditeNotes from './AddEditeNotes.vue';
import type { Note } from '../../Types/Note';
import { VueFinalModal } from 'vue-final-modal';

const noteStore = useNoteStore();
const searchQuery = ref('');
const isModalOpen = ref(false);
const selectedNote = ref<Note | null>(null);
const sortOption = ref<'latest' | 'oldest' | 'title'>('latest'); // sorting option

onMounted(() => {
  noteStore.fetchNotes();
});

const filteredNotes = computed(() => {
  let result = noteStore.notes;

  // Filter by search
  if (searchQuery.value.trim()) {
    const q = searchQuery.value.toLowerCase();
    result = result.filter((note) =>
      note.title.toLowerCase().includes(q) ||
      note.content.toLowerCase().includes(q)
    );
  }

  // Sort  option
  switch (sortOption.value) {
    case 'latest':
      result = result.slice().sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
      break;
    case 'oldest':
      result = result.slice().sort((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime());
      break;
    case 'title':
      result = result.slice().sort((a, b) => a.title.localeCompare(b.title));
      break;
  }

  return result;
});

function handleEdit(note: Note) {
  selectedNote.value = note;
  isModalOpen.value = true;
}

function openAddModal() {
  selectedNote.value = null;
  isModalOpen.value = true;
}

const overlayStyle = computed(() => ({
  backgroundColor: isModalOpen.value ? 'rgba(0, 0, 0, 0.5)' : 'transparent',
}));

const modalContentClasses = computed(() =>
  `fixed bottom-0 left-0 right-0 bg-white rounded-t-lg p-5 max-h-[75vh] overflow-auto
   sm:rounded-md sm:mx-auto sm:mt-14 sm:max-w-[40%]`
);
</script>

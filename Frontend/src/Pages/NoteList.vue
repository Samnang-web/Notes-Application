<template>
  <div class="min-h-screen bg-gray-50 p-6">
    <!-- Header -->
    <div class="flex flex-col md:flex-row md:justify-between md:items-center gap-4 mb-6">
      <h1 class="text-3xl font-bold text-indigo-700">My Notes</h1>
      <div class="flex gap-4">
        <button @click="logout"
                class="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700">
          Logout
        </button>
      </div>
    </div>

    <!-- Search + Sort -->
    <div class="flex flex-col md:flex-row gap-4 mb-6">
      <input v-model="searchQuery"
             placeholder="Search notes..."
             class="w-full md:w-1/2 p-3 border rounded focus:outline-none focus:ring focus:border-indigo-500" />
      <select v-model="sortOption" class="w-full md:w-1/4 p-3 border rounded">
        <option value="latest">Latest</option>
        <option value="oldest">Oldest</option>
        <option value="title">Title A-Z</option>
      </select>
      <button @click="openCreateModal"
          class="bg-indigo-600 text-white px-4 py-2 rounded hover:bg-indigo-700">
          Add Note
      </button>
    </div>

    

    <!-- Notes Cards -->
    <div class="grid md:grid-cols-3 gap-6">
      <div
        v-for="note in filteredNotes"
        :key="note.id"
        class="bg-white shadow p-4 rounded-lg border cursor-pointer hover:bg-gray-100 transition"
        @click="viewDetail(note.id)"
      >
        <h2 class="text-lg font-semibold text-gray-800">{{ note.title }}</h2>
        <p class="text-sm text-gray-600 mt-2 truncate">{{ note.content }}</p>

        <div class="flex justify-between text-xs text-gray-400 mt-3 select-none">
          <span>🕒 Created Date: {{ formatDate(note.createdAt) }}</span>
        </div>
        <div class="flex justify-between items-center text-xs text-gray-400 mt-3 select-none">
          <span>🔄 Updated Date: {{ formatDate(note.updatedAt) }}</span>
          <div class="flex gap-3 ml-4">
            <button @click.stop="editNote(note)" class="text-blue-600 hover:underline">
              Edit
            </button>
            <button @click.stop="deleteNote(note.id)" class="text-red-600 hover:underline">
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>

    <NoteModal
      v-if="showModal"
      :note="selectedNote"
      @close="closeModal"
      @saved="store.fetchNotes"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import NoteModal from '../components/NoteModal.vue'
import { useNoteStore } from '../Stores/NoteStore'
import { useAuthStore } from '../Stores/Auth'
import type { Note } from '../Types/Note'

const store = useNoteStore()
const authStore = useAuthStore()
const router = useRouter()

const showModal = ref(false)
const selectedNote = ref<Note | null>(null)
const searchQuery = ref('')
const sortOption = ref<'latest' | 'oldest' | 'title'>('latest')

function openCreateModal() {
  selectedNote.value = null
  showModal.value = true
}

function editNote(note: Note) {
  selectedNote.value = note
  showModal.value = true
}

function closeModal() {
  showModal.value = false
}

function deleteNote(id: number) {
  store.removeNote(id)
}

function viewDetail(id: number) {
  router.push({ name: 'NoteDetail', params: { id } })
}

function formatDate(iso: string | undefined | null): string {
  if (!iso) return 'N/A'
  const d = new Date(iso)
  return isNaN(d.getTime()) ? 'Invalid date' : d.toLocaleString()
}

function logout() {
  authStore.logout()
  router.push('/')
}

// Filter and sort notes computed
const filteredNotes = computed(() => {
  let filtered = store.notes

  if (searchQuery.value.trim()) {
    const q = searchQuery.value.toLowerCase()
    filtered = filtered.filter(note =>
      note.title?.toLowerCase().includes(q) || note.content?.toLowerCase().includes(q)
    )
  }

  switch (sortOption.value) {
    case 'oldest':
      return filtered.slice().sort((a, b) =>
        new Date(a.createdAt ?? '').getTime() - new Date(b.createdAt ?? '').getTime()
      )
    case 'title':
      return filtered.slice().sort((a, b) =>
        (a.title ?? '').localeCompare(b.title ?? '')
      )
    case 'latest':
    default:
      return filtered.slice().sort((a, b) =>
        new Date(b.createdAt ?? '').getTime() - new Date(a.createdAt ?? '').getTime()
      )
  }
})

onMounted(() => {
  store.fetchNotes()
})
</script>

<template>
  <div class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
    <div class="bg-white rounded-lg shadow-lg w-full max-w-md p-6 relative">
      <h2 class="text-xl font-bold text-indigo-700 mb-4">
        {{ note?.id ? 'Edit Note' : 'Create Note' }}
      </h2>

      <form @submit.prevent="handleSave">
        <div class="mb-4">
          <label class="block text-gray-700 mb-1">Title</label>
          <input
            v-model="form.title"
            type="text"
            placeholder="Enter title"
            required
            class="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-indigo-500"
          />
        </div>

        <div class="mb-4">
          <label class="block text-gray-700 mb-1">Content</label>
          <textarea
            v-model="form.content"
            placeholder="Enter content"
            required
            rows="4"
            class="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-indigo-500"
          ></textarea>
        </div>

        <div class="flex justify-end gap-3">
          <button type="button"
                  @click="$emit('close')"
                  class="px-4 py-2 rounded bg-gray-200 hover:bg-gray-300">
            Cancel
          </button>

          <button type="submit"
                  class="px-4 py-2 rounded bg-indigo-600 text-white hover:bg-indigo-700">
            {{ note?.id ? 'Update' : 'Create' }}
          </button>
        </div>
      </form>

      <!-- Close X Icon -->
      <button @click="$emit('close')" class="absolute top-2 right-2 text-gray-500 hover:text-gray-700">
        ✕
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, defineEmits, defineProps } from 'vue'
import { createNote, updateNote } from '../Services/NoteServices'
import type { Note } from '../Types/Note'

const props = defineProps<{ note: Note | null }>()
const emit = defineEmits(['close', 'saved'])

const form = ref<Partial<Note>>({
  title: '',
  content: ''
})

// Sync form data when editing existing note
watch(
  () => props.note,
  (newNote) => {
    if (newNote) {
      form.value = {
        title: newNote.title,
        content: newNote.content
      }
    } else {
      form.value = {
        title: '',
        content: ''
      }
    }
  },
  { immediate: true }
)

async function handleSave() {
  if (props.note?.id) {
    await updateNote(props.note.id, form.value)
  } else {
    await createNote(form.value)
  }

  emit('saved')
  emit('close')
}
</script>

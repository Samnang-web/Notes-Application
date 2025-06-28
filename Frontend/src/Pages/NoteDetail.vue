<template>
  <div class="min-h-screen bg-gray-50 py-10 px-4">
    <div class="max-w-3xl mx-auto bg-white rounded-lg shadow-lg p-8">
      <h1 class="text-4xl font-extrabold text-indigo-700 mb-6 break-words">
        {{ note?.title || 'Untitled Note' }}
      </h1>

      <p class="text-gray-700 whitespace-pre-line leading-relaxed text-lg mb-8 break-words">
        {{ note?.content || 'No content available.' }}
      </p>

      <div class="flex flex-col sm:flex-row sm:justify-between text-sm text-gray-500 mb-6 space-y-2 sm:space-y-0">
        <div>🕒 Created: {{ formatDate(note?.createdAt) }}</div>
        <div>🔄 Updated: {{ formatDate(note?.updatedAt) }}</div>
      </div>

      <button
        @click="$router.back()"
        class="inline-block px-6 py-3 bg-indigo-600 text-white font-semibold rounded-md shadow hover:bg-indigo-700 transition"
      >
        ← Back to Notes
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import type { Note } from "../Types/Note";
import { getNoteId } from "../Services/NoteServices";

const route = useRoute();
const note = ref<Note | null>(null);

const formatDate = (iso: string | undefined | null) => {
  if (!iso) return "N/A";
  const d = new Date(iso);
  return isNaN(d.getTime()) ? "Invalid date" : d.toLocaleString();
};

onMounted(async () => {
  const noteId = route.params.id;
  if (!noteId || Array.isArray(noteId)) return;

  try {
    const idNumber = Number(noteId);
    note.value = await getNoteId(idNumber);
  } catch (error) {
    console.error("Failed to load note:", error);
    note.value = null;
  }
});
</script>

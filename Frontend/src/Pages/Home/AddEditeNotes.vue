<template>
  <div class="bg-slate-100 justify-center rounded shadow ">
    <div class="flex flex-col gap-2">
      <label class="input-label text-slate-700 p-3">TITLE</label>
      <input  
        v-model="title"
        type="text"
        class="text-sm text-slate-950 outline-none bg-slate-100 border pl-3 rounded"
        placeholder="Title"
      />
    </div>

    <div class="flex flex-col gap-2 mt-4">
      <label class="input-label text-slate-700 p-3">CONTENT</label>
      <textarea 
        v-model="content"
        class="text-sm text-slate-950 outline-none bg-slate-100 pl-3 border rounded"
        placeholder="Content"
        rows="7"
      ></textarea>
    </div>

    <button
      class="btn-primary font-medium mt-5 p-3"
      @click="handleSaveNote"
    >
      {{ props.note ? 'UPDATE' : 'ADD' }}
    </button>
  </div>
</template>


<script setup lang="ts">
import { ref, watch,  } from 'vue';
import { useNoteStore } from '../../Stores/Note.store';
import type { Note } from '../../Types/Note';

const props = defineProps<{ note: Note | null }>();
const emit = defineEmits(['close']);

const title = ref('');
const content = ref('');
const noteStore = useNoteStore();

// ✅ Fill inputs when editing
watch(
  () => props.note,
  (newNote) => {
    title.value = newNote?.title || '';
    content.value = newNote?.content || '';
  },
  { immediate: true }
);

const handleSaveNote = async () => {
  if (!title.value.trim() || !content.value.trim()) return;

  if (props.note) {
    // ✅ UPDATE
    await noteStore.editNote(props.note.id, {
      title: title.value,
      content: content.value,
    });
  } else {
    // ✅ CREATE
    await noteStore.addNote({
      title: title.value,
      content: content.value,
      userId: 1,
    });
  }

  emit('close'); // ✅ Close modal after save
};
</script>


<template>
  <div
    class="border rounded p-4 bg-white hover:shadow-xl transition-all ease-in-out cursor-pointer"
    @click="toggleExpanded"
  >
    <div class="flex items-center justify-between">
      <div>
        <h6 class="text-sm font-medium">{{ note.title }}</h6>
        <span class="text-xs text-slate-500">Created: {{ formatDate(note.createdAt) }}</span>
      </div>
    </div>

    <p class="text-xs text-slate-600 mt-2">
      {{ expanded ? note.content : note.content.slice(0, 60) + (note.content.length > 60 ? '...' : '') }}
    </p>

    <div class="flex items-center justify-between mt-2">
      <div class="text-xs text-slate-500">Last updated: {{ formatDate(note.updatedAt) }}</div>
      <div class="flex items-center gap-2">
        <Icon
          icon="mdi:edit"
          width="24"
          height="24"
          class="hover:text-green-600 cursor-pointer"
          @click.stop="editNote"
        />
        <Icon
          icon="mdi:delete"
          width="24"
          height="24"
          class="hover:text-red-500 cursor-pointer"
          @click.stop="deleteNote"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { Icon } from '@iconify/vue';
import type { Note } from '../../Types/Note';

const props = defineProps<{ note: Note }>();

const expanded = ref(false);

const formatDate = (isoDate: string | null | undefined): string => {
  if (!isoDate) return 'N/A';
  const date = new Date(isoDate);
  return isNaN(date.getTime()) ? 'Invalid date' : date.toLocaleString();
};

const emit = defineEmits(['onEdit', 'onDelete']);

function toggleExpanded() {
  expanded.value = !expanded.value;
}

function editNote() {
  emit('onEdit', props.note);
}

function deleteNote() {
  if (confirm('Are you sure you want to delete this note?')) {
    emit('onDelete', props.note.id);
  }
}
</script>

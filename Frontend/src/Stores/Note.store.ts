import { defineStore } from 'pinia';
import { getNotes, createNote, updateNote, deleteNote } from '../Services/Note.services';
import type  { Note } from '../Types/Note';

export const useNoteStore = defineStore('note', {
  state: () => ({
    notes: [] as Note[],
  }),
  actions: {
    async fetchNotes() {
      this.notes = await getNotes();
    },
    async addNote(note: Partial<Note>) {
      await createNote(note);
      await this.fetchNotes();
    },
    async editNote(id: number, note: Partial<Note>) {
      await updateNote(id, note);
      await this.fetchNotes();
    },
    async removeNote(id: number) {
      await deleteNote(id);
      await this.fetchNotes();
    },
  },
});

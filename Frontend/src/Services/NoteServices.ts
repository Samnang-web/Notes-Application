import axios from './api';
import type { Note } from '../Types/Note';

export const getNotes = async (): Promise<Note[]> =>{
  const response = await axios.get<Note[]>('/note',);
  return response.data;
};

// other API calls remain unchanged
export const getNoteId = async (id: number): Promise<Note> => {
  const response = await axios.get(`/note/${id}`);
  return response.data;
};

export const createNote = async (note: Partial<Note>) => {
  const response = await axios.post('/note', note);
  return response.data;
};

export const updateNote = async (id: number, note: Partial<Note>) => {
  const response = await axios.put(`/note/${id}`, note);
  return response.data;
};

export const deleteNote = async (id: number) => {
  const response = await axios.delete(`/note/${id}`);
  return response.data;
};

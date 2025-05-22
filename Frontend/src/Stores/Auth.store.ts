import { defineStore } from 'pinia';
import { login, register } from '../Services/Auth.services';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || '',
    username: localStorage.getItem('username') || '',
  }),
  actions: {
    async login(username: string, password: string) {
      const data = await login(username, password);
      this.token = data.token;
      this.username = username;

      localStorage.setItem('token', data.token);
      localStorage.setItem('username', username);
    },
    async register(username: string, password: string) {
      await register(username, password);
    },
    logout() {
      this.token = '';
      this.username = '';
      localStorage.removeItem('token');
      localStorage.removeItem('username');
    },
  },
});


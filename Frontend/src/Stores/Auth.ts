import { defineStore } from 'pinia'
import { login, register } from '../Services/AuthServices'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || '',
    username: localStorage.getItem('username') || '',
  }),
  actions: {
    async login(email: string, password: string) {
      const data = await login(email, password)
      this.token = data.token
      this.username = data.username

      localStorage.setItem('token', data.token)
      localStorage.setItem('username', data.username)
    },

    async register(username: string, email: string, password: string) {
      await register(username, email, password)
    },

    logout() {
      this.token = ''
      this.username = ''
      localStorage.removeItem('token')
      localStorage.removeItem('username')
    },
  },
})
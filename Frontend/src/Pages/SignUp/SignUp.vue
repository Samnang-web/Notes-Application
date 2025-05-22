<template>
  <Navbar/>
  <div class="flex item-center justify-center mt-28">
    <div class="w-96 border rounded bg-white px-7 py-10"> 
      <form @submit.prevent="handleRegister">
        <h4 class="text-2xl mb-7">Register</h4>
        <input v-model="username" placeholder="Name" class="input-box" />
        <input v-model="password"  placeholder="Password" class="input-box" />

        <button type="submit" class="btn-primary">
          Create an account
        </button>

        <p class="text-sm text-center mt-4 " v-if="successMessage" style="color:green;">
          {{ successMessage }}
        </p>
        <p class="text-sm text-center mt-4 " v-if="errorMessage" style="color:red;">
          {{ errorMessage }}
        </p>

        <p class="text-sm text-center mt-4 ">
          Already have an account?
          <router-link to="/" class="font-medium text-primary underline">
            Login
          </router-link>
        </p>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../../Stores/Auth.store'

const router = useRouter()
const authStore = useAuthStore()

const username = ref('')
const password = ref('')

const successMessage = ref('')
const errorMessage = ref('')

const handleRegister = async () => {
  successMessage.value = ''
  errorMessage.value = ''

  try {
    await authStore.register(username.value, password.value)
    successMessage.value = 'Registered successfully! Redirecting to login...'
    setTimeout(() => {
      router.push('/')
    }, 1000)
  } catch (err: any) {
    errorMessage.value = err.response?.data || 'Something went wrong.'
  }
}
</script>

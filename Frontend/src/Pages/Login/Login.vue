<template>


    <div class="flex item-center justify-center mt-28">
        <div class="w-96 border rounded bg-white px-7 py-10"> 
            <form @submit.prevent="handleLogin">
                <h4 class="text-2xl mb-7">Login</h4>
                <input v-model="username" type="text" placeholder="Username" class="input-box" required/>
                <input v-model="password" type="password" placeholder="Password" class="input-box" required/>
                <button type="submit" class="btn-primary">
                    Login
                </button>

                <p v-if="errorMessage" class="text-red-600 mt-3 text-center">{{ errorMessage }}</p>

                <p class="text-sm text-center mt-4 ">
                    Not Registered yet?
                    <router-link to="/signup" class="font-medium text-primary underline">
                        Create an account
                    </router-link>
                </p>
            </form>
        </div>
    </div>
</template>
<script setup lang="ts">
    import { ref } from 'vue';
    import { useAuthStore } from '../../Stores/Auth.store';
    import { useRouter } from 'vue-router';

    const username = ref('');
    const password = ref('');
    const authStore = useAuthStore();
    const router = useRouter();
    const errorMessage = ref('')

    const handleLogin = async () => {
        errorMessage.value = ''
        try{
            await authStore.login(username.value, password.value);
            router.push('/dashboard');
        }catch (err: any){
            errorMessage.value = typeof err.response?.data === 'string'
            ? err.response.data
            : err.response?.data?.message || 'Something went wrong!'
        }
    };
</script>
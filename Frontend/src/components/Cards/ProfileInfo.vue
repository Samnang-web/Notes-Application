<template>
    <div class="flex item-center gap-3">
        <div class="w-12 h-12 flex items-center justify-center rounded-full text-slate-950 font-medium bg-slate-100">
            {{userInitials}}
        </div>

        <div>
            <p class="text-sm font-medium">{{authStore.username}}</p>
            <button class="text-sm text-slate-700 underline" @click="logout">
                Logout
            </button>
        </div>
    </div>
</template>

<script setup >
    import {useRouter} from 'vue-router';
    import { useAuthStore } from '../../Stores/Auth.store';

    const router = useRouter();
    const authStore = useAuthStore();
    const userInitials = authStore.username
        .split(' ')
        .map(word => word[0])
        .join('')
        .toUpperCase();

        function logout() {
            authStore.logout();
            router.push('/');
        }
</script>
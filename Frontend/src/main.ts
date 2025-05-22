import { createApp } from 'vue';
import './style.css'
import App from './App.vue';
import { createVfm } from 'vue-final-modal';
import router from './Router/router';

// ✅ 1. Import pinia
import { createPinia } from 'pinia';

const app = createApp(App);

// ✅ 2. Create pinia instance
const pinia = createPinia();

// ✅ 3. Use plugins in correct order
app.use(pinia); // <-- Must add this!
app.use(router);
app.use(createVfm());

app.mount('#app');

// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import Login from '../Pages/Login/Login.vue';
import Home from '../Pages/Home/Home.vue';
import SignUp from '../Pages/SignUp/SignUp.vue';

const routes = [
  {
    path: '/dashboard',
    name: 'Home',
    component: Home,
  },
  {
    path: '/',
    name: 'Login',
    component: Login,
  },
  {
    path: '/SignUp',
    name: 'SignUp',
    component: SignUp,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;

// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import NoteList from '../Pages/NoteList.vue';
import NoteDetail from '../Pages/NoteDetail.vue';
import Login from '../Pages/Login/Login.vue';
import SignUp from '../Pages/SignUp/SignUp.vue';
import NoteModal from '../components/NoteModal.vue';


const routes = [
  {
    path: '/note',
    name: 'NoteList',
    component: NoteList,
  },
   { path: '/note/:id', component: NoteDetail, name: 'NoteDetail' },
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
  {
    path: '/notemodal',
    name: 'NotedModal',
    component: NoteModal,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;

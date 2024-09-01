import { createRouter, createWebHistory } from 'vue-router';

import Home from '@/pages/Home.vue';
import Produtos from '@/pages/Produtos.vue';

const routes = [
  {
    path: '/',
    component: Home,
  },
  {
    path: '/Produtos',
    component: Produtos,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;

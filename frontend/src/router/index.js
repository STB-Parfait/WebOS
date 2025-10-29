import HomeView from '@/views/HomeView.vue'
import UserRegister from '@/views/UserRegister.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "register",
      component: UserRegister
    }
  ],
})

export default router

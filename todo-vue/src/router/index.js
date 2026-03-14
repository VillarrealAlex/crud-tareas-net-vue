import { createRouter, createWebHistory } from "vue-router";
import Login from "../components/Login.vue";
import Signup from "../components/Signup.vue";
import Dashboard from "../views/Dashboard.vue";

const routes = [
  { path: "/login", component: Login },
  { path: "/signup", component: Signup },
  { 
    path: "/", 
    component: Dashboard,
    meta: { requiresAuth: true } // ruta protegida
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

// Antes de cada navegación, verificamos si la ruta requiere login
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem("token");

  if (to.meta.requiresAuth && !token) {
    // Si la ruta requiere login y no hay token, redirigimos al login
    next("/login");
  } else {
    next();
  }
});

export default router;

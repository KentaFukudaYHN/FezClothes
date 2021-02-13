import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/pages/LoginPage.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'Login',
        componet: Login
    }
]

const router = new VueRouter({
    mode: 'history',
    routes: routes
})

export default router
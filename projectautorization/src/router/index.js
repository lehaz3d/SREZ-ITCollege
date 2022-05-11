import { createRouter, createWebHistory } from 'vue-router'
import MainWindow from '../components/MainWindow'
import SignUp from '../components/SignUp'
import LoginWindow from '../components/LoginWindow'
import ChangePassword from '../components/ChangePassword'
import RestorePassword from '../components/RestorePassword'

const routes = [
  {
    path: '/',
    name: 'login',
    component: LoginWindow
  },
  {
    path: '/sign',
    name: 'sign',
    component: SignUp
  },
  {
    path: '/main/:userId',
    name: 'main',
    component: MainWindow
  },
  {
    path: '/change',
    name: 'change',
    component: ChangePassword
  },
  {
    path: '/restore',
    name: 'restore',
    component: RestorePassword
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
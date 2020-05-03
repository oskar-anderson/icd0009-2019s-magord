import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

import AccountLogin from '../views/Account/Login.vue'
import AccountRegister from '../views/Account/Register.vue'

import AccountManage from '../views/Account/Manage/Index.vue'
import AccountManageEmail from '../views/Account/Manage/Email.vue'
import AccountManagePassword from '../views/Account/Manage/Password.vue'

import TownsIndex from '../views/Towns/Index.vue'
import TownsDetails from '../views/Towns/Details.vue'
import TownsEdit from '../views/Towns/Edit.vue'
import TownsCreate from '../views/Towns/Create.vue'

import PersonsIndex from '../views/Persons/Index.vue'
import PersonsDetails from '../views/Persons/Details.vue'
import PersonsEdit from '../views/Persons/Edit.vue'
import PersonsCreate from '../views/Persons/Create.vue'

import DrinksIndex from '../views/Drinks/Index.vue'
import DrinksDetails from '../views/Drinks/Details.vue'
import DrinksEdit from '../views/Drinks/Edit.vue'
import DrinksCreate from '../views/Drinks/Create.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [

    { path: '/', name: 'Home', component: Home },

    { path: '/account/login', name: 'AccountLogin', component: AccountLogin },
    { path: '/account/register', name: 'AccountRegister', component: AccountRegister },

    { path: '/account/manage/index', name: 'AccountManage', component: AccountManage },
    { path: '/account/manage/email', name: 'AccountManageEmail', component: AccountManageEmail },
    { path: '/account/manage/password', name: 'AccountManagePassword', component: AccountManagePassword },

    { path: '/towns', name: 'Towns', component: TownsIndex },
    { path: '/towns/details/:id?', name: 'TownsDetails', component: TownsDetails, props: true, meta: { title: 'Details' } },
    { path: '/towns/edit/:id?', name: 'TownsEdit', component: TownsEdit, props: true, meta: { title: 'Edit' } },
    { path: '/towns/create', name: 'TownsCreate', component: TownsCreate, meta: { title: 'Create' } },

    { path: '/persons/', name: 'Persons', component: PersonsIndex },
    { path: '/persons/details/:id?', name: 'PersonsDetails', component: PersonsDetails, props: true },
    { path: '/persons/edit/:id?', name: 'PersonsEdit', component: PersonsEdit, props: true, meta: { title: 'Edit' } },
    { path: '/persons/create', name: 'PersonsCreate', component: PersonsCreate, meta: { title: 'Create' } },

    { path: '/drinks/', name: 'Drinks', component: DrinksIndex },
    { path: '/drinks/details/:id?', name: 'DrinksDetails', component: DrinksDetails, props: true },
    { path: '/drinks/edit/:id?', name: 'DrinksEdit', component: DrinksEdit, props: true, meta: { title: 'Edit' } },
    { path: '/drinks/create', name: 'DrinksCreate', component: DrinksCreate, meta: { title: 'Create' } }
]

const router = new VueRouter({
    routes
})

export default router

import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'

import AccountLogin from '../views/Account/Login.vue'
import AccountRegister from '../views/Account/Register.vue'

import AccountManage from '../views/Account/Manage/Index.vue'
import AccountManageEmail from '../views/Account/Manage/Email.vue'
import AccountManagePassword from '../views/Account/Manage/Password.vue'
import AccountManagePhoneNumber from '../views/Account/Manage/PhoneNumber.vue'

import DrinksIndex from '../views/Drinks/Index.vue'
import DrinksEdit from '../views/Drinks/Edit.vue'
import DrinksCreate from '../views/Drinks/Create.vue'

import PricesIndex from '../views/Prices/Index.vue'
import PricesEdit from '../views/Prices/Edit.vue'
import PricesCreate from '../views/Prices/Create.vue'

import CampaignsIndex from '../views/Campaigns/Index.vue'
import CampaignsEdit from '../views/Campaigns/Edit.vue'
import CampaignsCreate from '../views/Campaigns/Create.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [

    { path: '/', name: 'Home', component: Home },

    { path: '/account/login', name: 'AccountLogin', component: AccountLogin },
    { path: '/account/register', name: 'AccountRegister', component: AccountRegister },

    { path: '/account/manage/index', name: 'AccountManage', component: AccountManage },
    { path: '/account/manage/email', name: 'AccountManageEmail', component: AccountManageEmail },
    { path: '/account/manage/password', name: 'AccountManagePassword', component: AccountManagePassword },
    { path: '/account/manage/phoneNumber', name: 'AccountManagePhoneNumber', component: AccountManagePhoneNumber },

    { path: '/drinks/', name: 'Drinks', component: DrinksIndex },
    { path: '/drinks/edit/:id?', name: 'DrinksEdit', component: DrinksEdit, props: true, meta: { title: 'Edit' } },
    { path: '/drinks/create', name: 'DrinksCreate', component: DrinksCreate, meta: { title: 'Create' } },

    { path: '/prices/', name: 'Prices', component: PricesIndex },
    { path: '/prices/edit/:id?', name: 'PricesEdit', component: PricesEdit, props: true, meta: { title: 'Edit' } },
    { path: '/prices/create', name: 'PricesCreate', component: PricesCreate, meta: { title: 'Create' } },

    { path: '/campaigns/', name: 'Campaigns', component: CampaignsIndex },
    { path: '/campaigns/edit/:id?', name: 'CampaignsEdit', component: CampaignsEdit, props: true, meta: { title: 'Edit' } },
    { path: '/campaigns/create', name: 'CampaignsCreate', component: CampaignsCreate, meta: { title: 'Create' } }
]

const router = new VueRouter({
    routes
})

export default router

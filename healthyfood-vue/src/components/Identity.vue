<template>
    <ul class="navbar-nav">
        <template v-if="isAuthenticated">
            <li class="nav-item">
                <span class="nav-link text-dark" hidden> {{userRole}} </span>
                <router-link to="/account/manage/index" class="nav-link text"> {{userEmail}} </router-link>
            </li>
            <li class="nav-item">
                <a @click="logoutOnClick" class="nav-link text-dark" href>Logout</a>
            </li>
        </template>
        <template v-else>
            <router-link to="/account/register" class="nav-link text-dark">Register</router-link>
            <router-link to="/account/login" class="nav-link text-dark">Login</router-link>
        </template>
    </ul>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import store from '../store'
import router from '../router';
import JwtDecode from 'jwt-decode';

@Component
export default class Identity extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userEmail(): string {
        return store.getters.userEmail;
    }

    logoutOnClick(): void{
        store.dispatch('clearJwt')
        router.push('/');
    }

    get userRole(): string {
        if (store.state.jwt) {
            const decoded = JwtDecode(store.state.jwt) as Record<string, string>;
            let userRole = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            if (userRole.includes('Admin')) {
                userRole = 'Admin';
            }
            store.commit('setUserRole', userRole);
            return userRole;
        }
        return "";
    }
}
</script>

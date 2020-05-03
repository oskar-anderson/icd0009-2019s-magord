<template>
    <div>
        <h1>Towns</h1>
        <p>
            <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'TownsCreate', params: { }}">Create new</router-link>
        </p>
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>

                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="town in towns" :key="town.id">
                    <td>{{town.name}}</td>
                    <td>
                        <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'TownsEdit', params: {id: town.id } }">Edit</router-link>
                        <span v-if="userRole != null && userRole.includes('Admin')"> | </span>
                        <router-link :to="{ name: 'TownsDetails', params: {id: town.id } }">Details</router-link>
                        <button
                            style="float: right"
                            v-if="userRole != null && userRole.includes('Admin')"
                            @click="deleteOnClick(town)"
                            type="button"
                            class="btn btn-danger"
                        >Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { ITown } from "../../domain/ITown/ITown";
import store from "../../store";

@Component
export default class TownsIndex extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    get towns(): ITown[] {
        return store.state.towns;
    }

    deleteOnClick(town: ITown): void {
        store.dispatch("deleteTown", town.id);
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        console.log("mounted");
        store.dispatch("getTowns");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}
</script>

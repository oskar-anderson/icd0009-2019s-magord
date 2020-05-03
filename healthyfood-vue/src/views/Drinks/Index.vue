<template>
    <div>
        <h1>Drinks</h1>
        <p>
            <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'DrinksCreate', params: { }}">Create new</router-link>
        </p>
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Size</th>
                    <th>Amount</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="drink in drinks" :key="drink.id">
                    <td>{{drink.name}}</td>
                    <td>{{drink.size}}</td>
                    <td>{{drink.amount}}</td>
                    <td>
                        <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'DrinksEdit', params: {id: drink.id } }">Edit</router-link>
                        <span v-if="userRole != null && userRole.includes('Admin')"> | </span>
                        <router-link :to="{ name: 'DrinksDetails', params: {id: drink.id } }">Details</router-link>
                        <button
                            style="float: right"
                            v-if="userRole != null && userRole.includes('Admin')"
                            @click="deleteOnClick(drink)"
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
import { IDrink } from "../../domain/IDrink/IDrink";
import store from "../../store";

@Component
export default class DrinksIndex extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    get drinks(): IDrink[] {
        return store.state.drinks;
    }

    deleteOnClick(drink: IDrink): void {
        store.dispatch("deleteDrink", drink.id);
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
        store.dispatch("getDrinks");
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

<template>
    <div>
        <h1>Prices</h1>
        <p>
            <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'PricesCreate', params: { }}">Create new</router-link>
        </p>
        <table class="table">
            <thead>
                <tr>
                    <th>Value</th>
                    <th>From</th>
                    <th>To</th>
                    <th>Campaign</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="price in prices" :key="price.id">
                    <td>{{price.value.toFixed(2)}}€</td>
                    <td>{{price.from}}</td>
                    <td>{{price.to}}</td>
                    <td>{{price.campaign}}</td>
                    <td>
                        <router-link  class ="btn btn-primary active" v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'PricesEdit', params: {id: price.id } }">Edit</router-link>
                        <button
                            style="float: right"
                            v-if="userRole != null && userRole.includes('Admin')"
                            @click="deleteOnClick(price)"
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
import store from "../../store";
import { IPrice } from '../../domain/IPrice/IPrice';

@Component
export default class PricesIndex extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    get prices(): IPrice[] {
        return store.state.prices;
    }

    deleteOnClick(price: IPrice): void {
        store.dispatch("deletePrice", price.id);
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
        store.dispatch("getPrices");
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

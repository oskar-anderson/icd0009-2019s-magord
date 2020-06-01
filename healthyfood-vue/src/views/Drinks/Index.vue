<template>
    <div>
        <h1>Drinks</h1>
        <p>
            <router-link
                v-if="userRole != null && userRole.includes('Admin')"
                :to="{ name: 'DrinksCreate', params: { }}"
            >Create new</router-link>
        </p>
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Size</th>
                    <th>
                        <a style="margin-left:33px">Amount</a>
                    </th>
                    <th>Price</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="drink in drinks" :key="drink.id">
                    <td>{{drink.name}}</td>
                    <td>{{drink.size}}L</td>
                    <td>
                        <button
                            style="font-size: 10px; margin-right:30px"
                            class="btn btn-primary btn-sm"
                            v-on:click="decrement(drink.id)"
                            type="button"
                        >
                            <span class="fa fa-minus"></span>
                        </button>
                        {{ drink.amount }}
                        <button
                            style="font-size: 10px; margin-left:30px"
                            class="btn btn-primary btn-sm"
                            v-on:click="increment(drink.id)"
                            type="button"
                        >
                            <span class="fa fa-plus"></span>
                        </button>
                    </td>
                    <td>{{drink.price.toFixed(2)}}€</td>
                    <td>
                        <button
                            click.trigger="addToCart(drink)"
                            style="margin:auto; display:block"
                            type="button"
                            class="btn btn-success"
                        >Add to cart</button>
                    </td>
                    <td>
                        <router-link
                            class="btn btn-primary active"
                            v-if="userRole != null && userRole.includes('Admin')"
                            :to="{ name: 'DrinksEdit', params: {id: drink.id } }"
                        >Edit</router-link>
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

    decrement(id: string) {
        for (const drink of this.drinks) {
            if (drink.id === id) {
                if (drink.amount !== 1) {
                    --drink.amount;
                }
            }
        }
    }

    increment(id: string) {
        for (const drink of this.drinks) {
            if (drink.id === id) {
                ++drink.amount;
            }
        }
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

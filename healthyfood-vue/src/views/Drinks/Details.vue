<template>
    <div>
        <h1>Details</h1>
        <h4>Drink</h4>
        <hr />
        <dl class="row" v-if="loading === false">
            <dt class="col-sm-2">Name</dt>
            <dd class="col-sm-10">{{ drink.name }}</dd>
            <dt class="col-sm-2">Size</dt>
            <dd class="col-sm-10">{{ drink.size }}</dd>
            <dt class="col-sm-2">Amount</dt>
            <dd class="col-sm-10">{{ drink.amount }}</dd>
        </dl>
        <div>
            <router-link   v-if="loading === false && userRole != null && userRole.includes('Admin')" :to="{ name: 'DrinksEdit', params: {id: drink.id } }">Edit</router-link>
            <span v-if="userRole != null && userRole.includes('Admin')"> | </span>
            <router-link :to="{ name: 'Drinks' }" >Back to List</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IDrink } from "../../domain/IDrink/IDrink";
import store from "../../store";

@Component
export default class DrinksDetails extends Vue {
    @Prop()
    private id!: string;

    private loading = true;

    get drink(): IDrink | null {
        if (this.loading === false) {
            return store.state.drink;
        }
        return null;
    }

    get userRole(): string {
        return store.getters.userRole;
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
        store.dispatch("getDrink", this.id).then((doneLoading: boolean) => {
            if (doneLoading) {
                this.loading = false;
            } else {
                this.loading = true
            }
        });
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

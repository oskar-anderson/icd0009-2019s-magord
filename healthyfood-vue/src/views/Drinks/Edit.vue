<template>
    <div>
        <h1>Edit</h1>
        <h4>Drink</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label">Edit name</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="drink.name"
                        />
                        <label class="control-label">Edit size</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Size"
                            maxlength="256"
                            v-model="drink.size"
                        />
                        <label class="control-label">Edit amount</label>
                        <input class="form-control" type="text" id="Amount" v-model="drink.amount" />
                        <label class="control-label">Edit price. <b>Current price is {{drink.price }}€ </b></label>
                        <select
                            class="form-control"
                            type="text"
                            id="Comment"
                            maxlength="256"
                            v-model="drink.priceId"
                        >
                            <option v-bind:value="null">No price</option>
                            <option
                                v-for="price in prices"
                                :key="price.id"
                                v-bind:value="price.id"
                            >{{ price.value }}</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <input
                            type="submit"
                            @click="onSubmit($event)"
                            value="Submit"
                            class="btn btn-primary"
                        />
                    </div>
                    <div>
                        <router-link :to="{ name: 'Drinks' }">Back to List</router-link>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
/* eslint-disable @typescript-eslint/no-non-null-assertion */
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "../../store";
import router from "../../router";
import { IDrink } from "@/domain/IDrink/IDrink";
import { IPrice } from '@/domain/IPrice/IPrice';

@Component
export default class DrinkEdit extends Vue {
    @Prop()
    private id!: string;

    get drink(): IDrink | null {
        return store.state.drink;
    }

    get price(): IPrice | null {
        return store.state.price;
    }

    get prices(): IPrice[] | null {
        return store.state.prices;
    }

    onSubmit(event: Event): void | null {
        event.preventDefault();
        if (
            isNaN(Number(this.drink!.size)) ||
            isNaN(Number(this.drink!.amount))
        ) {
            alert("Please enter a number into size and amount!");
            return null;
        }
        if (this.drink!.size <= 0 || this.drink!.amount <= 0) {
            alert("Please enter a correct number!");
            return null;
        }
        console.log(event);
        if (
            this.drink!.name.length > 0 &&
            String(this.drink!.size).length > 0 &&
            String(this.drink!.amount).length > 0
        ) {
            this.drink!.size = Number(this.drink!.size);
            this.drink!.amount = Number(this.drink!.amount);
            store.dispatch("updateDrink", this.drink!);
            router.push("/drinks");
        }
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
        store.dispatch('getPrices');
        store.dispatch("getDrink", this.id);
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

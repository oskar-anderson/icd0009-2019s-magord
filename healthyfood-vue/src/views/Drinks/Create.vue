<template>
    <div>
        <h1>Create</h1>
        <h4>Drink</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="drinkInfo.name"
                        />
                        <label class="control-label" for="Size">Size</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Size"
                            maxlength="256"
                            v-model="drinkInfo.size"
                        />
                        <label class="control-label" for="Amount">Amount</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Amount"
                            maxlength="256"
                            v-model="drinkInfo.amount"
                        />
                        <label class="control-label" for="Price">Price</label>
                        <select
                            class="form-control"
                            type="text"
                            id="Comment"
                            maxlength="256"
                            v-model="drinkInfo.priceId"
                        >
                            <option value="null">No price</option>
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
                            value="Create"
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
import { Component, Vue } from "vue-property-decorator";
import store from "../../store";
import { IDrinkCreate } from "../../domain/IDrink/IDrinkCreate";
import router from "../../router";
import { IPrice } from "@/domain/IPrice/IPrice";

@Component
export default class DrinksCreate extends Vue {
    private drinkInfo: IDrinkCreate = {
        name: "",
        size: null,
        amount: null,
        priceId: null
    };

    get prices(): IPrice[] {
        return store.state.prices;
    }

    onSubmit(event: Event): void | null {
        event.preventDefault();
        if (
            isNaN(Number(this.drinkInfo.size)) ||
            isNaN(Number(this.drinkInfo.amount))
        ) {
            alert("Please enter a number into size and amount!");
            return null;
        }
        if (!this.isInt(Number(this.drinkInfo.amount))) {
            alert("Please enter correct amount!");
            return null;
        }
        if (this.drinkInfo.size! <= 0 || this.drinkInfo.amount! <= 0) {
            alert("Please enter a correct number!");
            return null;
        }
        if (this.drinkInfo.name.length > 0) {
            this.drinkInfo.size = Number(this.drinkInfo.size);
            this.drinkInfo.amount = Number(this.drinkInfo.amount);
            store.dispatch("createDrink", this.drinkInfo);
            router.push("/drinks");
        }
    }

    isInt(n: any): boolean {
        return n % 1 === 0;
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
        store.dispatch("getPrices");
        console.log("mounted");
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

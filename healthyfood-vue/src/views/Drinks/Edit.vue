<template>
    <div>
        <h1>Edit</h1>
        <h4>Drink</h4>
        <hr />
        <div class="row" v-if="this.loading === false">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label">Edit name. Current - <b>{{drink.name}}</b></label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="drinkInfo.name"
                        />
                        <label class="control-label">Edit size. Current - <b>{{drink.size}}</b></label>
                        <input
                            class="form-control"
                            type="text"
                            id="Size"
                            maxlength="256"
                            v-model="drinkInfo.size"
                        />
                        <label class="control-label">Edit amount. Current -<b>{{drink.amount}}</b></label>
                        <input
                            class="form-control"
                            type="text"
                            id="Amount"
                            v-model="drinkInfo.amount"
                        />
                    </div>
                    <div class="form-group">
                        <input @click="onSubmit($event)" value="Submit" class="btn btn-primary" />
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
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "../../store";
import router from "../../router";
import { IDrink } from "@/domain/IDrink/IDrink";
import { IDrinkEdit } from "../../domain/IDrink/IDrinkEdit";

@Component
export default class DrinkEdit extends Vue {
    @Prop()
    private id!: string;

    private loading = true;

    private drinkInfo: IDrinkEdit = {
        id: this.id,
        name: "",
        size: "",
        amount: ""
    }

    get drink(): IDrink | null {
        if (this.loading === false) {
            return store.state.drink;
        }
        return null;
    }

    onSubmit(event: Event): void | null {
        event.preventDefault();
        if (isNaN(Number(this.drinkInfo.size)) || isNaN(Number(this.drinkInfo.amount))) {
            alert("Please enter a number into size and amount!");
            return null;
        }
        if (this.drinkInfo.size <= 0 || this.drinkInfo.amount <= 0) {
            alert("Please enter a correct number!");
            return null;
        }
        console.log(event);
        if (this.drinkInfo.name.length > 0 && String(this.drinkInfo.size).length > 0 && String(this.drinkInfo.amount).length > 0) {
            this.drinkInfo.size = Number(this.drinkInfo.size)
            this.drinkInfo.amount = Number(this.drinkInfo.amount)
            store.dispatch("updateDrink", this.drinkInfo);
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
        store.dispatch("getDrink", this.id).then((doneLoading: boolean) => {
            if (doneLoading) {
                this.loading = false;
            } else {
                this.loading = true;
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

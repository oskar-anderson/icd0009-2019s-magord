<template>
    <div>
        <h1>Edit</h1>
        <h4>Price</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label">Edit value</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="price.value"
                        />
                        <label class="control-label">Edit from</label>
                        <input
                            class="form-control"
                            type="text"
                            id="From"
                            maxlength="256"
                            v-model="price.from"
                        />
                        <label class="control-label">Edit to</label>
                        <input class="form-control" type="text" id="To" v-model="price.to" />
                        <label class="control-label">
                            Edit campaign.
                            <div>
                                <b>Current campaign is: {{price.campaign }}</b>
                            </div>
                        </label>
                        <select
                            class="form-control"
                            type="text"
                            id="Comment"
                            maxlength="256"
                            v-model="price.campaignId"
                        >
                            <option v-bind:value="null">No campaign</option>
                            <option
                                v-for="campaign in campaigns"
                                :key="campaign.id"
                                v-bind:value="campaign.id"
                            >{{ campaign.name }}</option>
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
                        <router-link :to="{ name: 'Prices' }">Back to List</router-link>
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
import flatpickr from "flatpickr";
import { ICampaign } from "@/domain/ICampaign/ICampaign";
import { IPrice } from "@/domain/IPrice/IPrice";
require("flatpickr/dist/flatpickr.css");

@Component
export default class PriceEdit extends Vue {
    @Prop()
    private id!: string;

    get price(): IPrice | null {
        return store.state.price;
    }

    get campaign(): ICampaign | null {
        return store.state.campaign;
    }

    get campaigns(): ICampaign[] | null {
        return store.state.campaigns;
    }

    onSubmit(event: Event): void | null {
        event.preventDefault();
        if (
            this.price!.value !== null &&
            this.price!.from !== "" &&
            this.price!.to !== ""
        ) {
            this.price!.value = Number(this.price!.value);
            store.dispatch("updatePrice", this.price);
            router.push("/prices");
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
        store.dispatch("getPrice", this.id);
        store.dispatch("getCampaigns");
        flatpickr("#From, #To", {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "d.m.Y"
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

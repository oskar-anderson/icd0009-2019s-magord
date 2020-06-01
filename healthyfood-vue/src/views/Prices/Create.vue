<template>
    <div>
        <h1>Create</h1>
        <h4>Price</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label" for="Name">Value</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="priceInfo.value"
                        />
                        <label class="control-label" for="From">From</label>
                        <input
                            class="form-control"
                            type="text"
                            id="From"
                            maxlength="256"
                            v-model="priceInfo.from"
                        />
                        <label class="control-label" for="To">To</label>
                        <input
                            class="form-control"
                            type="text"
                            id="To"
                            maxlength="256"
                            v-model="priceInfo.to"
                        />
                        <label class="control-label" for="Comment">Campaign</label>
                        <select
                            class="form-control"
                            type="text"
                            id="Comment"
                            maxlength="256"
                            v-model="priceInfo.campaignId"
                        >
                        <option value=null>No campaign</option>
                            <option
                                v-for="campaign in campaigns"
                                :key="campaign.id"
                                v-bind:value= campaign.id
                            >{{ campaign.name }}</option>
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
                        <router-link :to="{ name: 'Prices' }">Back to List</router-link>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../../store";
import router from "../../router";
import flatpickr from "flatpickr";
import { IPriceCreate } from "../../domain/IPrice/IPriceCreate";
import { ICampaign } from "@/domain/ICampaign/ICampaign";
require("flatpickr/dist/flatpickr.css");

@Component
export default class PricesCreate extends Vue {
    private priceInfo: IPriceCreate = {
        value: null,
        from: "",
        to: "",
        campaignId: null
    };

    get campaigns(): ICampaign[] | null {
        return store.state.campaigns;
    }

    onSubmit(event: Event): void | null {
        event.preventDefault();
        if (this.priceInfo.value !== null && this.priceInfo.from !== "" && this.priceInfo.to !== "") {
            this.priceInfo.value = Number(this.priceInfo.value);
            store.dispatch("createPrice", this.priceInfo);
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
        flatpickr("#From, #To", {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "d.m.Y"
        });
        store.dispatch("getCampaigns");
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

<template>
    <div>
        <h1>Edit</h1>
        <h4>Campaign</h4>
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
                            v-model="campaign.name"
                        />
                        <label class="control-label">Edit from</label>
                        <input
                            class="form-control"
                            type="text"
                            id="From"
                            maxlength="256"
                            v-model="campaign.from"
                        />
                        <label class="control-label">Edit to</label>
                        <input class="form-control" type="text" id="To" v-model="campaign.to" />
                        <label class="control-label">Edit comment</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Comment"
                            v-model="campaign.comment"
                        />
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
                        <router-link :to="{ name: 'Campaigns' }">Back to List</router-link>
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
import { ICampaign } from "@/domain/ICampaign/ICampaign";
import { ICampaignEdit } from "../../domain/ICampaign/ICampaignEdit";
import flatpickr from "flatpickr";
require("flatpickr/dist/flatpickr.css");

@Component
export default class CampaignEdit extends Vue {
    @Prop()
    private id!: string;

    private campaignInfo: ICampaignEdit = {
        id: this.id,
        name: store.getters.campaign.name,
        from: store.getters.campaign.from,
        to: store.getters.campaign.to,
        comment: store.getters.campaign.comment
    };

    get campaign(): ICampaign | null {
        return store.state.campaign;
    }

    onSubmit(event: Event): void | null {
        event.preventDefault();
        if (
            this.campaign!.name.length > 0 &&
            this.campaign!.from.length > 0 &&
            this.campaign!.to.length > 0
        ) {
            store.dispatch("updateCampaign", this.campaign);
            router.push("/campaigns");
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
        store.dispatch("getCampaign", this.id);
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

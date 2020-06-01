<template>
    <div>
        <h1>Create</h1>
        <h4>Campaign</h4>
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
                            v-model="campaignInfo.name"
                        />
                        <label class="control-label" for="From">From</label>
                        <input
                            class="form-control"
                            type="text"
                            id="From"
                            maxlength="256"
                            v-model="campaignInfo.from"
                        />
                        <label class="control-label" for="To">To</label>
                        <input
                            class="form-control"
                            type="text"
                            id="To"
                            maxlength="256"
                            v-model="campaignInfo.to"
                        />
                        <label class="control-label" for="Comment">Comment</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Comment"
                            maxlength="256"
                            v-model="campaignInfo.comment"
                        />
                    </div>
                    <div class="form-group">
                        <input type="submit" @click="onSubmit($event)" value="Create" class="btn btn-primary" />
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
import { Component, Vue } from "vue-property-decorator";
import store from "../../store";
import { ICampaignCreate } from '../../domain/ICampaign/ICampaignCreate';
import router from '../../router';
import flatpickr from 'flatpickr';
require("flatpickr/dist/flatpickr.css")

@Component
export default class CampaignsCreate extends Vue {
    private campaignInfo: ICampaignCreate = {
        name: "",
        from: "",
        to: "",
        comment: ""
    }

    onSubmit(event: Event): void | null {
        event.preventDefault();
        if (this.campaignInfo.name.length > 0 && this.campaignInfo.from.length > 0 && this.campaignInfo.to.length > 0) {
            store.dispatch("createCampaign", this.campaignInfo)
            router.push("/campaigns")
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
        flatpickr('#From, #To', {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "d.m.Y"
        })
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

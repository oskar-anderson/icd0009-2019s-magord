<template>
    <div>
        <h1>Campaings</h1>
        <p>
            <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'CampaignsCreate', params: { }}">Create new</router-link>
        </p>
        <table class="table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>From</th>
                    <th>To</th>
                    <th>Description</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="campaign in campaigns" :key="campaign.id">
                    <td>{{campaign.name}}</td>
                    <td>{{campaign.from}}</td>
                    <td>{{campaign.to}}</td>
                    <td>{{campaign.comment}}</td>
                    <td>
                        <router-link  class ="btn btn-primary active" v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'CampaignsEdit', params: {id: campaign.id } }">Edit</router-link>
                        <button
                            style="float: right"
                            v-if="userRole != null && userRole.includes('Admin')"
                            @click="deleteOnClick(campaign)"
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
import { ICampaign } from "../../domain/ICampaign/ICampaign";
import store from "../../store";

@Component
export default class CampaignsIndex extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    get campaigns(): ICampaign[] {
        return store.state.campaigns;
    }

    deleteOnClick(campaign: ICampaign): void {
        store.dispatch("deleteCampaign", campaign.id);
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

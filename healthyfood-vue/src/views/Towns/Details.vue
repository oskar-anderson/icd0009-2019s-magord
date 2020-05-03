<template>
    <div>
        <h1>Details</h1>
        <h4>Town</h4>
        <hr />
        <dl class="row" v-if="loading === false">
            <dt class="col-sm-2">Name</dt>
            <dd class="col-sm-10">{{ town.name }}</dd>
        </dl>
        <div>
            <router-link v-if="loading === false && userRole != null && userRole.includes('Admin')" :to="{ name: 'TownsEdit', params: {id: town.id } }">Edit</router-link>
            <span v-if="userRole != null && userRole.includes('Admin')"> | </span>
            <router-link :to="{ name: 'Towns' }" >Back to List</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { ITown } from "../../domain/ITown/ITown";
import store from "../../store";

@Component
export default class TownsDetails extends Vue {
    @Prop()
    private id!: string;

    private loading = true

    get town(): ITown | null {
        if (this.loading === false) {
            return store.state.town;
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
        store.dispatch("getTown", this.id).then((doneLoading: boolean) => {
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

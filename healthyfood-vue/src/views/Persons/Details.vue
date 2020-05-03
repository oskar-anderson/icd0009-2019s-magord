<template>
    <div>
        <h1>Details</h1>
        <h4>Person</h4>
        <hr />
        <dl class="row" v-if="loading === false">
            <dt class="col-sm-2">First name</dt>
            <dd class="col-sm-10">{{ person.firstName }}</dd>
            <dt class="col-sm-2">Last name</dt>
            <dd class="col-sm-10">{{ person.lastName }}</dd>
            <dt class="col-sm-2">Sex</dt>
            <dd class="col-sm-10">{{ person.sex }}</dd>
            <dt class="col-sm-2">Date of birth</dt>
            <dd class="col-sm-10">{{ person.dateOfBirth }}</dd>
        </dl>
        <div>
            <router-link  v-if="loading === false && userRole != null && userRole.includes('Admin')" :to="{ name: 'PersonsEdit', params: {id: person.id } }">Edit</router-link>
            <span v-if="userRole != null && userRole.includes('Admin')"> | </span>
            <router-link :to="{ name: 'Persons' }" >Back to List</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IPerson } from "../../domain/IPerson/IPerson";
import store from "../../store";

@Component
export default class PersonsDetails extends Vue {
    @Prop()
    private id!: string;

    private loading = true;

    get person(): IPerson | null {
        if (this.loading === false) {
            return store.state.person;
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
        store.dispatch("getPerson", this.id).then((doneLoading: boolean) => {
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

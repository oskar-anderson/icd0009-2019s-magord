<template>
    <div>
        <h1>Persons</h1>
        <p>
            <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'PersonsCreate', params: { }}">Create new</router-link>
        </p>
        <table class="table">
            <thead>
                <tr>
                    <th>First name</th>
                    <th>Last name</th>
                    <th>Sex</th>
                    <th>Date of birth</th>

                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="person in persons" :key="person.id">
                    <td>{{person.firstName}}</td>
                    <td>{{person.lastName}}</td>
                    <td>{{person.sex}}</td>
                    <td>{{person.dateOfBirth}}</td>
                    <td>
                        <router-link v-if="userRole != null && userRole.includes('Admin')" :to="{ name: 'PersonsEdit', params: {id: person.id } }">Edit</router-link>
                        <span v-if="userRole != null && userRole.includes('Admin')"> | </span>
                        <router-link :to="{ name: 'PersonsDetails', params: {id: person.id } }">Details</router-link>
                        <button
                            style="float: right"
                            v-if="userRole != null && userRole.includes('Admin')"
                            @click="deleteOnClick(person)"
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
import { IPerson } from "../../domain/IPerson/IPerson";
import store from "../../store";

@Component
export default class PersonsIndex extends Vue {
    get isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    get persons(): IPerson[] {
        return store.state.persons;
    }

    deleteOnClick(person: IPerson): void {
        store.dispatch("deletePerson", person.id);
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
        store.dispatch("getPersons");
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

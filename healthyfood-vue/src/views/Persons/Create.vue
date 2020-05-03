<template>
    <div>
        <h1>Create</h1>
        <h4>Person</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label" for="FirstName">First name</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="personInfo.firstName"
                        />
                        <label class="control-label" for="LastName">Last name</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="personInfo.lastName"
                        />
                        <label class="control-label" for="Sex">Sex</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="1"
                            v-model="personInfo.sex"
                        />
                        <label class="control-label" for="DateOfBirth">Date of birth</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="personInfo.dateOfBirth"
                        />
                    </div>
                    <div class="form-group">
                        <input @click="onSubmit($event)" value="Create" class="btn btn-primary" />
                    </div>
                    <div>
                        <router-link :to="{ name: 'Persons' }">Back to List</router-link>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../../store";
import { IPersonCreate } from '../../domain/IPerson/IPersonCreate';
import router from '../../router';

@Component
export default class PersonsCreate extends Vue {
    private personInfo: IPersonCreate = {
        firstName: "",
        lastName: "",
        sex: "",
        dateOfBirth: ""
    }

    onSubmit(event: Event): void {
        console.log(event);
        if (this.personInfo.firstName.length > 0 && this.personInfo.lastName.length > 0 && this.personInfo.sex.length > 0 && this.personInfo.dateOfBirth.length > 0) {
            store.dispatch("createPerson", this.personInfo)
            router.push("/persons")
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

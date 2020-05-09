<template>
    <div>
        <h1>Edit</h1>
        <h4>Person</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label" for="FirstName" v-if="this.loading === false">
                            Edit first name. Current -
                            <b>{{person.firstName}}</b>
                        </label>
                        <input
                            class="form-control"
                            type="text"
                            id="FirstName"
                            maxlength="256"
                            v-model="personInfo.firstName"
                        />
                        <label class="control-label" for="LastName" v-if="this.loading === false">
                            Edit last name. Current -
                            <b>{{person.lastName}}</b>
                        </label>
                        <input
                            class="form-control"
                            type="text"
                            id="LastName"
                            maxlength="256"
                            v-model="personInfo.lastName"
                        />
                        <label class="control-label" for="Sex" v-if="this.loading === false">
                            Edit sex. Current -
                            <b>{{person.sex}}</b>
                        </label>
                        <input
                            class="form-control"
                            type="text"
                            id="Sex"
                            maxlength="1"
                            v-model="personInfo.sex"
                        />
                        <label class="control-label" for="DateOfBirth" v-if="this.loading === false">
                            Edit date of birth. Current -
                            <b>{{person.dateOfBirth}}</b>
                        </label>
                        <input
                            class="form-control"
                            type="text"
                            id="dob"
                            maxlength="256"
                            v-model="personInfo.dateOfBirth"
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
                        <router-link :to="{ name: 'Persons' }">Back to List</router-link>
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
import { IPerson } from "@/domain/IPerson/IPerson";
import { IPersonEdit } from "../../domain/IPerson/IPersonEdit";
import flatpickr from "flatpickr";
require("flatpickr/dist/flatpickr.css");

@Component
export default class PersonsEdit extends Vue {
    @Prop()
    private id!: string;

    private loading = true;

    private personInfo: IPersonEdit = {
        id: this.id,
        firstName: "",
        lastName: "",
        sex: "",
        dateOfBirth: ""
    };

    get person(): IPerson | null {
        if (this.loading === false) {
            return store.state.person;
        }
        return null;
    }

    onSubmit(event: Event): void {
        console.log(event);
        if (
            this.personInfo.firstName.length > 0 &&
            this.personInfo.lastName.length > 0 &&
            this.personInfo.sex.length > 0 &&
            this.personInfo.dateOfBirth.length > 0
        ) {
            store.dispatch("updatePerson", this.personInfo);
            router.push("/persons");
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
        flatpickr("#dob", {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "Y-m-d"
        });
        store.dispatch("getPerson", this.id).then((doneLoading: boolean) => {
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

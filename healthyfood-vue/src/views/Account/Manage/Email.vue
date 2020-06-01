<template>
    <div>
        <h1>Manage your account</h1>
        <h4>Change your account settings</h4>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <ul class="nav nav-pills flex-column">
                    <li class="nav-item">
                        <a>
                            <router-link
                                to="/account/manage/index"
                                class="nav-link text-dark"
                            >Profile</router-link>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a>
                            <router-link
                                to="/account/manage/email"
                                class="nav-link active text-dark"
                            >Change email</router-link>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a>
                            <router-link
                                to="/account/manage/password"
                                class="nav-link text-dark"
                            >Change password</router-link>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a>
                            <router-link
                                to="/account/manage/phoneNumber"
                                class="nav-link text-dark"
                            >Change phone number</router-link>
                        </a>
                    </li>
                </ul>
            </div>
            <div class="col-md-9">
                <h4>Manage Email</h4>
                <div class="row">
                    <div class="col-md-6">
                        <form id="email-form">
                            <div
                                class="text-danger validation-summary-valid"
                                data-valmsg-summary="true"
                            >
                                <ul>
                                    <li style="display:none"></li>
                                </ul>
                            </div>
                            <div class="form-group">
                                <label for="Email">Email</label>
                                <input
                                    class="form-control"
                                    disabled
                                    type="text"
                                    data-val="true"
                                    id="Email"
                                    name="Email"
                                    v-model="changeEmailInfo.email"
                                />
                            </div>
                            <div class="form-group">
                                <label for="Input_NewEmail">New email</label>
                                <input
                                    class="form-control"
                                    type="email"
                                    id="Input_NewEmail"
                                    name="Input.NewEmail"
                                    v-model="changeEmailInfo.newEmail"
                                />
                                <span
                                    class="text-danger field-validation-valid"
                                    data-valmsg-for="Input.NewEmail"
                                    data-valmsg-replace="true"
                                ></span>
                            </div>
                            <button
                                @click="changeEmail($event)"
                                type="submit"
                                class="btn btn-primary"
                            >Change email</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../../../store";
import { IChangeEmailDTO } from "@/types/IChangeEmailDTO";
import router from "@/router";

@Component
export default class Login extends Vue {
    private changeEmailInfo: IChangeEmailDTO = {
        email: this.userEmail,
        newEmail: ""
    };

    private changeWasOk: boolean | null = null;

    changeEmail(): void {
        if (this.changeEmailInfo.newEmail.length > 0) {
            store.dispatch("changeEmail", this.changeEmailInfo);
            store.commit('setUserEmail', this.changeEmailInfo.newEmail)
            router.push("/account/manage/index")
            alert("Email changed!")
        }
    }

    get userEmail(): string {
        return store.getters.userEmail;
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

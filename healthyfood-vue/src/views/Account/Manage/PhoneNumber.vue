<template>
    <div>
        <h1>Manage your account</h1>

        <div>
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
                                    class="nav-link text-dark"
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
                                    class="nav-link active text-dark"
                                >Change phone number</router-link>
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-9">
                    <h4>Change phone number</h4>

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
                                    <label for="Email">Phone number</label>
                                    <input
                                        class="form-control"
                                        disabled
                                        type="text"
                                        data-val="true"
                                        id="Email"
                                        name="Email"
                                        v-model="changePhoneNumberInfo.phoneNumber"
                                    />
                                </div>
                                <div class="form-group">
                                    <label for="Input_NewEmail">New phone number</label>
                                    <input
                                        class="form-control"
                                        type="email"
                                        id="Input_NewEmail"
                                        name="Input.NewEmail"
                                        v-model="changePhoneNumberInfo.newPhoneNumber"
                                    />
                                    <span
                                        class="text-danger field-validation-valid"
                                        data-valmsg-for="Input.NewEmail"
                                        data-valmsg-replace="true"
                                    ></span>
                                </div>
                                <button
                                    @click="changePhoneNumber($event)"
                                    type="submit"
                                    class="btn btn-primary"
                                >Update</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../../../store";
import router from "@/router";
import { IChangePhoneNumberDTO } from '../../../types/IChangePhoneNumberDTO';

@Component
export default class Login extends Vue {
    private changePhoneNumberInfo: IChangePhoneNumberDTO = {
        email: this.userEmail,
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        phoneNumber: localStorage.getItem('phoneNumber')!,
        newPhoneNumber: ""
    };

    private changeWasOk: boolean | null = null;

    changePhoneNumber(event: Event): void | null {
        event.preventDefault();
        store.dispatch("changePhoneNumber", this.changePhoneNumberInfo);
        localStorage.setItem('phoneNumber', this.changePhoneNumberInfo.newPhoneNumber)
        store.commit("setPhoneNumber", this.changePhoneNumberInfo.newPhoneNumber);
        router.push("/account/manage/index");
        alert("Phone number changed!");
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

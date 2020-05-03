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
                                    class="nav-link active text-dark"
                                >Change password</router-link>
                            </a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-9">
                    <h4>Change password</h4>

                    <div class="row">
                        <div class="col-md-6">
                            <form id="change-password-form">
                                <div
                                    class="text-danger validation-summary-valid"
                                    data-valmsg-summary="true"
                                >
                                    <ul>
                                        <li style="display:none"></li>
                                    </ul>
                                </div>
                                <div class="form-group">
                                    <label for="Input_OldPassword">Current password</label>
                                    <input
                                        class="form-control"
                                        type="password"
                                        data-val="true"
                                        name="Input.OldPassword"
                                        v-model="changePasswordInfo.oldPassword"
                                    />
                                    <span
                                        class="text-danger field-validation-valid"
                                        data-valmsg-for="Input.OldPassword"
                                        data-valmsg-replace="true"
                                    ></span>
                                </div>
                                <div class="form-group">
                                    <label for="Input_NewPassword">New password</label>
                                    <input
                                        class="form-control"
                                        type="password"
                                        data-val="true"
                                        id="Input_NewPassword"
                                        name="Input.NewPassword"
                                        v-model="changePasswordInfo.newPassword"
                                    />
                                    <span
                                        class="text-danger field-validation-valid"
                                        data-valmsg-for="Input.NewPassword"
                                        data-valmsg-replace="true"
                                    ></span>
                                </div>
                                <div class="form-group">
                                    <label for="Input_ConfirmPassword">Confirm new password</label>
                                    <input
                                        class="form-control"
                                        type="password"
                                        data-val="true"
                                        v-model="confirmPassword"
                                    />
                                    <span
                                        class="text-danger field-validation-valid"
                                        data-valmsg-for="Input.ConfirmPassword"
                                        data-valmsg-replace="true"
                                    ></span>
                                </div>
                                <button @click="changePassword($event)" type="submit" class="btn btn-primary">Update password</button>
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
import { IChangePasswordDTO } from "@/types/IChangePasswordDTO";
import router from "@/router";

@Component
export default class Login extends Vue {
    private changePasswordInfo: IChangePasswordDTO = {
        email: this.userEmail,
        oldPassword: this.password,
        newPassword: ""
    };

    private confirmPassword= ""

    private changeWasOk: boolean | null = null;

    changePassword(event: Event): void | null {
        event.preventDefault();
        if (this.changePasswordInfo.oldPassword === this.changePasswordInfo.newPassword) {
            alert("New password can't be the same as current one!");
            return null;
        }
        if (this.changePasswordInfo.oldPassword !== this.password) {
            alert("You have entered the wrong current password");
            return null;
        }
        if (this.changePasswordInfo.newPassword !== this.confirmPassword) {
            alert("Passwords don't match");
            return null;
        }
        if (this.checkForValidPassword(this.changePasswordInfo.newPassword) === false) {
            return null;
        }
        store.dispatch("changePassword", this.changePasswordInfo);
        store.commit("setPassword", this.changePasswordInfo.newPassword);
        router.push("/account/manage/index");
        alert("Password changed!");
    }

    get userEmail(): string {
        return store.getters.userEmail;
    }

    get password(): string {
        return store.getters.password
    }

    checkForValidPassword(password: string) {
        let answer = true;

        if (password.length < 6) {
            alert("Password is too short!")
            answer = false;
        }

        let lowerCounter = 0
        let upperCounter = 0

        for (let i = 0; i < password.length; i++) {
            const element = password[i];
            if (/\d/.test(element) === false && element === element.toLowerCase()) {
                lowerCounter++
            }
            if (/\d/.test(element) === false && element === element.toUpperCase()) {
                upperCounter++
            }
        }

        if (lowerCounter === 0) {
            alert("Password must include lowercase letter!")
            answer = false;
        }

        if (upperCounter === 0) {
            alert("Password must include uppercase letter!")
            answer = false;
        }

        const isDigit = /\d/.test(password)

        if (isDigit === false) {
            alert("Password must include a digit!")
            answer = false;
        }

        const isAlphanumeric = /^[a-z0-9]+$/i.test(password)

        if (isAlphanumeric !== false) {
            alert("Passowrd must include a nonalphanumeric symbol!")
            answer = false;
        }

        return answer;
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

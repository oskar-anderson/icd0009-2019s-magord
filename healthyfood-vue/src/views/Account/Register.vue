<template>

    <div class="row">
        <div class="col-md-4">
            <form>
                <h4>Create a new account</h4>
                <hr />
                <h4 style="color: red" v-if="registerWasOk === false">Registration failed!</h4>
                <div class="form-group">
                    <label for="Input_Email">Email</label>
                    <input class="form-control" type="email" v-model="registerInfo.email" />
                </div>

                <div class="form-group">
                    <label for="Input_FirstName">First name</label>
                    <input class="form-control" type="firstName" v-model="registerInfo.firstName" />
                </div>

                <div class="form-group">
                    <label for="Input_LastName">Last name</label>
                    <input class="form-control" type="lastName" v-model="registerInfo.lastName" />
                </div>

                <div class="form-group">
                    <label for="Input_Password">Phone number <br><i><b>(Example 5306945)</b></i></label>
                    <input class="form-control" type="phoneNumber" v-model="registerInfo.phoneNumber" />
                </div>

                <div class="form-group">
                    <label for="Input_Password">Password</label>
                    <input class="form-control" type="password" v-model="registerInfo.password" />
                </div>

                <div class="form-group">
                    <label for="Input_Password">Confirm password</label>
                    <input
                        class="form-control"
                        type="password"
                        v-model="confirmPassword"
                    />
                </div>

                <div class="form-group">
                    <button
                        type="submit"
                        @click="registerOnClick($event)"
                        class="btn btn-primary"
                    >Register</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "../../router";
import { IRegisterDTO } from "@/types/IRegisterDTO";
import store from "../../store";

@Component
export default class Register extends Vue {
    private registerInfo: IRegisterDTO = {
        email: "",
        firstName: "",
        lastName: "",
        phoneNumber: "",
        password: ""
    };

    private confirmPassword = "";

    private registerWasOk: boolean | null = null;

    checkForValidPassword(password: string) {
        let answer = true;

        if (password.length < 6) {
            alert("Password is too short!")
            this.registerWasOk = false
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
            this.registerWasOk = false
            answer = false;
        }

        if (upperCounter === 0) {
            alert("Password must include uppercase letter!")
            this.registerWasOk = false
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
            this.registerWasOk = false
            answer = false;
        }

        return answer;
    }

    registerOnClick(event: Event) {
        event.preventDefault();
        if (this.registerInfo.password !== this.confirmPassword) {
            alert("Passwords don't match!");
            this.registerWasOk = false
            return null;
        }
        if (!(this.checkForValidPassword(this.registerInfo.password))) {
            return null;
        }
        store.dispatch("registerUser", this.registerInfo).then((isLoggedIn: boolean) => {
            if (isLoggedIn) {
                store.commit("setPhoneNumber", this.registerInfo);
                this.registerWasOk = true;
                router.push("/");
                alert("Registration successful!")
            } else {
                this.registerWasOk = false;
            }
        })
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

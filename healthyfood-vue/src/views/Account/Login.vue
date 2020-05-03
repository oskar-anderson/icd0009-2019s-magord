<template>
    <div class="row">
        <div class="col-md-6">
            <form>
                <h4>Use a local account to log in.</h4>
                <h2 style="color: red" v-if="loginWasOk === false">Login failed!</h2>
                <hr />
                <div class="form-group">
                    <label for="Input_Email">Email</label>
                    <input v-model="loginInfo.email" class="form-control" type="email" id="Input_Email" />
                </div>
                <div class="form-group">
                    <label for="Input_Password">Password</label>
                    <input
                        v-model="loginInfo.password"
                        class="form-control"
                        type="password"
                        id="Input_Password"
                    />
                </div>
                <div class="form-group">
                    <button type="submit" @click="loginOnClick($event)" class="btn btn-primary">Log in</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "../../router";
import { ILoginDTO } from "@/types/ILoginDTO";
import store from "../../store";

@Component
export default class Login extends Vue {
    public loginInfo: ILoginDTO = {
        email: "",
        password: ""
    };

    private loginWasOk: boolean | null = null;

    loginOnClick(event: Event): void | null {
        event.preventDefault();
        if (this.loginInfo.email.length < 0 || this.loginInfo.password.length < 0) {
            this.loginWasOk = false;
            return null;
        }
        store
            .dispatch("authenticateUser", this.loginInfo)
            .then((isLoggedIn: boolean) => {
                if (isLoggedIn) {
                    this.loginWasOk = true;
                    router.push("/");
                } else {
                    this.loginWasOk = false;
                }
            });
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

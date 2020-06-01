<template>
    <div>
        <h1>Manage your account</h1>
        <h4>Change your account settings</h4>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <ul class="nav nav-pills flex-column">
                    <li class="nav-item">
                        <a><router-link to="/account/manage/index" class="nav-link active text-dark">Profile</router-link> </a>
                    </li>
                    <li class="nav-item">
                        <a><router-link to="/account/manage/email" class="nav-link text-dark">Change email</router-link> </a>
                    </li>
                    <li class="nav-item">
                        <a><router-link to="/account/manage/password" class="nav-link text-dark">Change password</router-link> </a>
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

                <h4>Profile</h4>

                <div class="row">
                    <div class="col-md-6">
                        <form id="profile-form" method="post">
                            <div class="text-danger validation-summary-valid" data-valmsg-summary="true">
                                <ul>
                                    <li style="display:none"></li>
                                </ul>
                            </div>

                            <div class="form-group">
                                <label for="Input_Email">Your username</label>
                                <input class="form-control" disabled type="text" v-model="userEmail"/>
                            </div>
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

@Component
export default class Login extends Vue {
    get userEmail() {
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

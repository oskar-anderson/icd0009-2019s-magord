<template>
    <div>
        <header>
            <nav
                class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3"
            >
                <div class="container">
                    <router-link to="/" class="navbar-brand">Home</router-link>
                    <button
                        class="navbar-toggler"
                        type="button"
                        data-toggle="collapse"
                        data-target=".navbar-collapse"
                        aria-controls="navbarSupportedContent"
                        aria-expanded="false"
                        aria-label="Toggle navigation"
                    >
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <Identity />
                        <ul class="navbar-nav flex-grow-1">
                            <li class="nav-item">
                                <router-link to="/campaigns" class="nav-link text-dark">Campaigns</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/drinks" class="nav-link text-dark">Drinks</router-link>
                            </li>
                            <li class="nav-item">
                                <router-link to="/prices" class="nav-link text-dark">Prices</router-link>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        <div class="container">
            <main role="main" class="pb-3">
                <router-view />
            </main>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Identity from "./components/Identity.vue";

@Component({
    components: {
        Identity
    }
})
export default class App extends Vue {}
</script>

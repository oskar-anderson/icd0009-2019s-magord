<template>
    <div>
        <h1>Create</h1>
        <h4>Town</h4>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label" for="Name">Name</label>
                        <input
                            class="form-control"
                            type="text"
                            id="Name"
                            maxlength="256"
                            v-model="townInfo.name"
                        />
                    </div>
                    <div class="form-group">
                        <input type="submit" @click="onSubmit($event)" value="Create" class="btn btn-primary" />
                    </div>
                    <div>
                        <router-link :to="{ name: 'Towns' }">Back to List</router-link>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "../../store";
import { ITownCreate } from '../../domain/ITown/ITownCreate';
import router from '../../router';

@Component
export default class TownsCreate extends Vue {
    private townInfo: ITownCreate = {
        name: ""
    }

    onSubmit(event: Event): void {
        console.log(event);
        if (this.townInfo.name.length > 0) {
            console.log(this.townInfo.name)
            store.dispatch("createTown", this.townInfo)
            router.push("/towns")
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

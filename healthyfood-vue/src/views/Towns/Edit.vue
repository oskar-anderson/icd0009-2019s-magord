<template>
    <div>
        <h1>Edit</h1>
        <h4>Town</h4>
        <hr />
        <div class="row" v-if="this.loading === false">
            <div class="col-md-4">
                <form>
                    <div class="form-group">
                        <label class="control-label" for="Name">
                            Edit name. Current -
                            <b>{{town.name}}</b>
                        </label>
                        <input
                            class="form-control text-muted"
                            type="text"
                            id="Name"
                            onfocus="this.value=''"
                            value="New name"
                            v-model="townInfo.name"
                        />
                    </div>
                    <div class="form-group">
                        <input @click="onSubmit($event)" value="Submit" class="btn btn-primary" />
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
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "../../store";
import router from "../../router";
import { ITown } from "@/domain/ITown/ITown";
import { ITownEdit } from "../../domain/ITown/ITownEdit";

@Component
export default class TownsEdit extends Vue {
    @Prop()
    private id!: string;

    private loading = true;

    private townInfo: ITownEdit = {
        id: this.id,
        name: ""
    };

    get town(): ITown | null {
        if (this.loading === false) {
            return store.state.town;
        }
        return null;
    }

    onSubmit(event: Event): void {
        console.log(event);
        event.preventDefault();
        if (this.townInfo.name.length > 0) {
            console.log(this.townInfo.name);
            store.dispatch("updateTown", this.townInfo);
            router.push("/towns");
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
        store.dispatch("getTown", this.id).then((doneLoading: boolean) => {
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

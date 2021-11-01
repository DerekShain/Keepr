<template>
  <div class="row text-dark">
    <div class="col">
      <img :src="keep.img" class="img-fluid rounded-start" alt="...">
    </div>
    <div class="col-md-8">
      <div class="card-header">
        Views {{keep.views}}, Shares {{keep.shares}}, Keeps {{keep.keeps}}
      </div>
      <div class="card-body text-center">
        <h5 class="card-title">{{keep.name}}</h5>
        <p class="card-text">{{keep.description}}</p>
        <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
      </div>
    </div>
  </div>
</template>


<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
export default {
  props: {
    keep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
    },
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(props.keep.id);
            Pop.toats("Keep Deleted");
          }
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error);
        }
      },
    };
  },
};
</script>


<style lang="scss" scoped>

</style>
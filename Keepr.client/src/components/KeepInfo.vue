<template>
  <div class="row text-dark">
    <div class="col">
      <img :src="keep.img" class="img-fluid rounded-start" alt="...">
    </div>
    <div class="col-md-8">
      <div class="card-header bg-white mb-5">
        <i class="mdi mdi-eye f-18" title="views">{{keep.views}}</i>
        <i class=" mdi mdi-temperature-kelvin f-18" title="keeps">{{keep.keeps}}</i>
        <i class="mdi mdi-share-variant f-18" title="shares">{{keep.shares}}</i>
         
      </div>
      <div class="card-body text-center mx-5">
        <h5 class="card-title">{{keep.name}}</h5>
        <p class="card-text">{{keep.description}}</p>
        
      </div>
      <div class="card-footer bg-white mt-5 p-3">
        <button class="btn btn-outline bg-info">Add To Vault</button>
        <div class="" v-if="keep.creator">
      <router-link :to="{ name: 'Profile', params: {profileId: keep.creatorId}}" class="btn text-success lighten-30 selectable text-uppercase">

      <img :src="keep.creator.picture" class="rounded-circle user-img" alt="" />
          </router-link>
        </div>
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
.card-header{
  display: flex;
  justify-content: space-around;
}
.card-footer{
  display: flex;
  justify-content: space-between;
}
.user-img {
  height: 45px;
}
</style>
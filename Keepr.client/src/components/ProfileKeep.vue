<template>
  <div class="myCard text-white my-3">
  <img :src="profileKeep.img" class="card-img" alt="..." >
  <div class="card-img-overlay" data-bs-toggle="modal" :data-bs-target="'#k-modal-' + keep.id">
    <h5 class="card-title">{{profileKeep.name}}</h5>
  </div>
</div>
<Modal :id="'k-modal-' + keep.id" class="text-light">
    <template #modal-body>
      <KeepInfo :keep="keep" class="" />
    </template>
  </Modal>
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed, onMounted, watchEffect } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from '../utils/Logger';
import { profilesService } from '../services/ProfilesService';
import { useRoute } from 'vue-router';

export default {
  props: {
    profileKeep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
    },
  },
  setup(props) {
    // const route = useRoute();
    // watchEffect(async()=>{
    //   await profilesService.getById(route.params.profileId),
    //   await profilesService.getKeepsByProfileId(route.params.profileId),
    //   await profilesService.getVaultsByProfileId(route.params.profileId)
    // })
    return {
      profileKeeps: computed(() => AppState.profileKeeps),
      keep:computed(() => AppState.keep),
      keeps:computed(() => AppState.keeps)
      }
    }
  }

</script>

<style lang="scss">
.card-img-overlay {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: end;
}
.myCard {
  position: relative;
  display: flex;
  flex-direction: column;
  min-width: 0;
  word-wrap: break-word;

  background-clip: border-box;

  border-radius: 0.25 rem;
}

.card-img {
  height: inherit;
  object-fit: cover;
}
.user-img {
  height: 45px;
}
</style>

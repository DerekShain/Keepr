<template>
 <div class="col">
  <div class="myCard text-white m-2">
    <img :src="profileKeep.img" class="card-img" alt="..." />
    <div
      class="card-img-overlay"
      data-bs-toggle="modal"
      :data-bs-target="'#k-modal-' + profileKeep.id"
      @click="viewCount(profileKeep)"
    >
      <h5 class="card-title">{{ profileKeep.name }}</h5>
      <div class="" v-if="profileKeep.creator">
        <div class="" @click.stop="goToProfile()">
          <img
            :src="profileKeep.creator.picture"
            class="rounded-circle user-img selectable"
            alt=""
          />
        </div>
      </div>
    </div>
  </div>
  </div>
<Modal :id="'k-modal-' + profileKeep.id" class="text-light">
    <template #modal-body>
      <KeepInfo :keep="profileKeep" class="" />
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
import { Account } from '../models/Account';

export default {
  props: {
    profileKeep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
      keep:{
        type: Keep,
      },
      account:{
        type: Account
      }
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
      keeps:computed(() => AppState.keeps),
       async viewCount(profileKeep) {
        try {
          keep.views = keep.views + 1;
          await keepsService.keepInteractions(profileKeep);
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error);
        }
      },
      }
    }
  }

</script>

<style lang="scss" scoped>
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

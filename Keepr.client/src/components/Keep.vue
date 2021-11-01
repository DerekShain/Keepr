<template>
  <div class="myCard text-white m-2">
    <img :src="keep.img" class="card-img" alt="..." />
    <div class="card-img-overlay" data-bs-toggle="modal" :data-bs-target="'#k-modal-' + keep.id" @click="viewCount(keep)">
      <h5 class="card-title">{{ keep.name }}</h5>
        <div class="" v-if="keep.creator">
      <router-link :to="{ name: 'Profile', params: {profileId: keep.creatorId}}" class="btn text-success lighten-30 selectable text-uppercase">

      <img :src="keep.creator.picture" class="rounded-circle user-img" alt="" />
          </router-link>
        </div>
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
import { computed } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from '../utils/Logger';
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
      profile: computed(() => AppState.profile),
      profile: computed(() => AppState.profile),
      profileKeeps: computed(() => AppState.profileKeeps),
      keeps: computed(() => AppState.keeps),
      profileVaults: computed(() => AppState.profileVaults),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(props.keep.id);
            Pop.toast("Keep Deleted");
          }
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error);
        }
      },
      async viewCount(keep) {
        try {
          keep.views = keep.views + 1;
          await keepsService.keepInteractions(keep);
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error)
        }
      },
    };
  },
};
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

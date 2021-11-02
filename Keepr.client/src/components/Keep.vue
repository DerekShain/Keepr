<template>
<div class="col">
  <div class="myCard text-white m-2">
    <img :src="keep.img" class="card-img" alt="..." />
    <div class="card-img-overlay">
      <h5 class="card-title selectable" data-bs-toggle="modal"
      :data-bs-target="'#k-modal-' + keep.id"
      @click="viewCount(keep)">{{ keep.name }}</h5>
      <div class="" v-if="keep.creator">
        <div class="" @click.prevent="goToProfile()">
          <img
            :src="keep.creator.picture"
            class="rounded-circle user-img selectable"
            alt=""
          />
        </div>
      </div>
    </div>
  </div>
</div>
  <Modal :id="'k-modal-' + keep.id" class="text-light">
    <template #modal-body>
      <KeepInfo  :keep="keep"/>
    </template>
  </Modal>
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { Account } from "../models/Account";
import { router } from "../router";
import { Modal } from "bootstrap";
export default {
  props: {
    keep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
    },
    account: {
      type: Account,
    },
  },
  setup(props) {
    return {
      profile: computed(() => AppState.profile),
      profile: computed(() => AppState.profile),
      profileKeeps: computed(() => AppState.profileKeeps),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      profileVaults: computed(() => AppState.profileVaults),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            const modal = Modal.getOrCreateInstance(
              document.getElementById("k-modal-" + props.keep.id)
            );
            modal.hide();
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
          logger.log(error);
        }
      },
      async goToProfile() {
        router.push({
          name: "Profile",
          params: { profileId: props.keep.creatorId },
        });
        const modal = Modal.getOrCreateInstance(
          document.getElementById("k-modal-" + props.keep.id)
        );
        modal.hide();
      },
    };
  },
};
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

<template>
<div class="mb-2">
  <div class="myCard text-shadow ">
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
      <KeepInfo  :keep="keep" :account="account"/>
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
            const modal = Modal.getOrCreateInstance(document.getElementById("k-modal-" + props.keep.id));
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
        const modal = Modal.getOrCreateInstance(document.getElementById("k-modal-" + props.keep.id));
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
  min-width: 0;
  word-wrap: break-word;
  background-clip: border-box;
  border-radius: 35px;
}
.myCard:hover {
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.5);
}
.card-img {
  height: inherit;
  object-fit: cover;
  border-radius: 35px;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
}
.card-img:hover{
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
}
.user-img {
  height: 45px;
}
.text-shadow{
  color: white;
  text-shadow: 2px 2px 4px #000000;
}
</style>

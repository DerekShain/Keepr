<template>
  <div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">{{keep.name}}</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <a href="#" class="btn btn-primary">Go somewhere</a>
  </div>
</div>
  
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from '../utils/Logger';
import { Vault } from "../models/Vault";
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
      // async deleteKeep() {
      //   try {
      //     if (await Pop.confirm()) {
      //       await keepsService.deleteKeep(props.vault.id);
      //       Pop.toast("Keep Deleted");
      //     }
      //   } catch (error) {
      //     Pop.toast(error.message, "error");
      //     logger.log(error);
      //   }
      // },
      // async viewCount(vault) {
      //   try {
      //     vault.views = vault.views + 1;
      //     await vaultsService.vaultInteractions(vault);
      //   } catch (error) {
      //     Pop.toast(error.message, "error");
      //     logger.log(error)
      //   }
      // },
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

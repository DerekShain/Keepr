<template>
  <div class="myCard text-white m-2">
  <img :src="keep.img" class="card-img" alt="..." >
  <div class="card-img-overlay" data-bs-toggle="modal" :data-bs-target="'#k-modal-' + keep.id" @click="viewCount(keep)">
    <h5 class="card-title">{{keep.name}}</h5>
    <div v-if="vault.creator.id === account.id">

    <i
            class="mdi mdi-delete-forever selectable text-danger f-20 mx-3"
            title="Remove Keep"
            @click.stop="removeKeep()"
          ></i>
    </div>
  </div>
</div>
 <Modal :id="'k-modal-' + keep.id" class="text-light">
    <template #modal-body>
      <KeepInfo :account="account" :keep="keep"/>
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
import { vaultKeepsService } from '../services/VaultKeepsService';
import { Modal} from 'bootstrap';

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
   const route = useRoute();
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
       async removeKeep() {
        try {
          if (await Pop.confirm()) {
            // debugger
            const modal = Modal.getOrCreateInstance(document.getElementById('k-modal-' + props.keep.id));
            modal.hide()
            await vaultKeepsService.deleteVaultKeep(props.keep.vaultKeepId)
            Pop.toast("Keep Deleted")
       
          }
        } catch (error) {
          Pop.toast("Error Deleting Vault", "error");
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
    }
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
</style>

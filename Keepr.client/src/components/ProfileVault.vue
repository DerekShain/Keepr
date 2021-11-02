<template>
  <div class="card shadow m-2" style="width: 18rem">
    <div class="card-body">
      <h5 class="card-title">{{ profileVault.name }}</h5>
      <p class="card-text">{{ profileVault.description }}</p>
      <div v-if="profileVault.isPrivate === true">
        <b>Private Vault!</b>
      </div>
      <div v-else>
        <b>Public Vault!</b>
      </div>
      <div class="d-flex">
        <router-link
          :to="{ name: 'Vault', params: { vaultId: profileVault.id }, query: {creatorId: account.id} }"
          class="btn btn-dark lighten-30 selectable text-uppercase"
        >
          GO TO VAULT
        </router-link>
        <div class="" v-if="profileVault.creatorId === account.id">
          <i
            class="mdi mdi-delete-forever selectable text-danger f-20 mx-3"
            title="Delete Post"
            @click="deleteVault()"
          ></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed, onMounted } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { profilesService } from "../services/ProfilesService";
import { Vault } from "../models/Vault";
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService";
import { Account } from '../models/Account';

export default {
  props: {
    profileVault: {
      type: Vault,
      default: () => {
        return new Vault();
      },
    },
    account:{
      type: Account
    }
  },
  setup(props) {
    const route = useRoute();
    return {
      profileVaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      vaults: computed(() => AppState.vaults),
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(props.profileVault.id);
            Pop.toast("Vault Deleted");
          }
        } catch (error) {
          Pop.toast("Error Deleting Vault", "error");
          logger.log(error);
        }
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

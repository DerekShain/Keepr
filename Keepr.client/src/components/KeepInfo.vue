<template>
  <div class="row text-dark">
    <div class="col">
      <img :src="keep.img" class="img-fluid rounded-start" alt="..." />
    </div>
    <div class="col-md-8">
      <div class="card-header bg-white mb-5">
        <i class="mdi mdi-eye f-18" title="views">{{ keep.views }}</i>
        <i class="mdi mdi-temperature-kelvin f-18" title="keeps">{{
          keep.keeps
        }}</i>
        <i class="mdi mdi-share-variant f-18" title="shares">{{
          keep.shares
        }}</i>
      </div>
      <div class="card-body text-center mx-5">
        <h5 class="card-title">{{ keep.name }}</h5>
        <p class="card-text">{{ keep.description }}</p>
      </div>
      <div class="card-footer bg-white mt-5 p-3">
        <!-- NOTE ADD to VAULTS -->
        <div class="dropdown">
          <button
            class="btn btn-secondary dropdown-toggle"
            type="button"
            id="dropdownMenuButton1"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Add to Vault
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
            <li v-for="vault in profileVaults" :key="vault.id" @click="addToVault(vault, keep)" class="selectable">{{vault.name}}</li>
          </ul>
        </div>

        <!-- NOTE DELETE Button -->
        <div class="" v-if="keep.creatorId === account.id">
          <i
            class="mdi mdi-delete-forever selectable text-danger f-20 mx-3"
            title="Delete Post"
            @click="deleteKeep()"
          ></i>
        </div>
        <!-- NOTE creator info -->
        <div class="" v-if="keep.creator">
          <router-link
            :to="{ name: 'Profile', params: { profileId: keep.creatorId } }"
            class="btn text-success lighten-30 selectable text-uppercase"
          >
            <img
              :src="keep.creator.picture"
              class="rounded-circle user-img"
              alt=""
            />
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
import { Modal } from "bootstrap";
import { logger } from "../utils/Logger";
import { vaultsService } from '../services/VaultsService';
import { vaultKeepsService } from '../services/VaultKeepsService';
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
      profileVaults: computed(() => AppState.profileVaults),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(props.keep.id);
            Pop.toast("Keep Deleted");
            const modal = Modal.getInstance(
              document.getElementById("k-modal-" + !keep.id)
            );
            modal.hide();
          }
        } catch (error) {
          Pop.toast("Error Deleting Vault", "error");
          logger.log(error);
        }
      },
      async addToVault(vault, keep) {
        try {
          var vaultKeep = {
            vaultId: vault.id,
            keepId: keep.id,
          };
          await vaultKeepsService.createVaultKeep(vaultKeep);
          Pop.toast("Keep Added to Vault");
        } catch (error) {
          Pop.toast("Error Adding Keep to Vault", "error");
          logger.log(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.card-header {
  display: flex;
  justify-content: space-around;
}
.card-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.user-img {
  height: 45px;
}
</style>

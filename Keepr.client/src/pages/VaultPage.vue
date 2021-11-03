<template>
  <div class="component">
    
    <div class="m-5"><h1>{{vault.name}}</h1>
    <h3>Keeps: {{ keep.length }}</h3> 
    </div>
    <div class="d-flex m-2" v-if="vault.creatorId === account.id">
      <button class="btn btn-dark text-light" @click="deleteVault()">Delete Vault</button>
    </div>
    <div class="">
    <div class="m-2 masonry-with-columns">
      <VaultKeep v-for="k in keep" :key="k.id" :keep="k" />
    </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Vault } from "../models/Vault";
import { AppState } from "../AppState";
import { vaultKeepsService } from "../services/VaultKeepsService";
import Pop from "../utils/Pop";
import { onMounted, watchEffect } from "@vue/runtime-core";
import { logger } from "../utils/Logger";
import { useRoute } from "vue-router";
import { Keep } from "../models/Keep";
import { profilesService } from "../services/ProfilesService";
import { vaultsService } from '../services/VaultsService';
import { router } from '../router';
import { Account } from '../models/Account';
export default {
  name: "Vault",
  props: {
    keep: {
      type: Keep,
      default: () => {
        return new Keep();
      }
    },
      vault:{
        type: Vault
      },
      account: {
        type: Account
        
      },
      
    },
  
  setup(props) {
    const route = useRoute();
    onMounted(() => {
      try {
        vaultKeepsService.getById(route.params.vaultId),
        vaultsService.getById(route.params.vaultId),
        profilesService.getById(route.params.profileId),
        profilesService.getKeepsByProfileId(route.params.profileId),
        profilesService.getVaultsByProfileId(route.params.profileId)
      } catch (error) {
        Pop.toast(error.message, "error");
        logger.log(error);
      }
    });
    return {
      keep: computed(() => AppState.keep),
      profileVaults: computed(() => AppState.profileVaults),
      profileVault: computed(() => AppState.profileVault),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      vaults: computed(() => AppState.vaults),
      vaultKeep: computed(() => AppState.vaultKeep),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      async viewCount(keep) {
        try {
          keep.views = keep.views + 1;
          await keepsService.keepInteractions(keep);
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error);
        }
      },
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(route.params.vaultId);
            Pop.toast("Vault Deleted");
            router.push({ name: "Profile", params: { profileId: AppState.account.id}})
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
.masonry-with-columns {
  columns: 6 300px;
  column-gap: 1rem;
  div {
    width: 200px;
    // background: #EC985A;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }
  @for $i from 1 through 36 {
    div:nth-child(#{$i}) {
      $h: (random(400) + 100) + px;
      height: $h;
      line-height: $h;
    }
  }
}
</style>

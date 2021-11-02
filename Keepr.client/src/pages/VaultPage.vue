<template>
  <div class="component">

<div class="M-5 ">
  <h1>{{vault}}</h1>
</div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { Vault } from '../models/Vault';
import { AppState } from '../AppState';
import { vaultKeepsService} from '../services/VaultKeepsService';
import Pop from '../utils/Pop';
import { watchEffect } from '@vue/runtime-core';
import { logger } from '../utils/Logger';
import { useRoute } from 'vue-router';
export default {
  name: "Vault",
   props: {
    vault: {
      type: Vault
    },
  },
  setup(props){
    const route = useRoute();
    watchEffect(() => {
      try{
        vaultKeepsService.getById(route.params.vaultId);
      }catch (error){
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      profile: computed(() => AppState.profile),
      vault: computed(() => AppState.vault),
      keep: computed(() => AppState.keep),
      keepVaults: computed(() => AppState.keepVaults),
      keepVault: computed(() => AppState.keepVault),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileKeep: computed(() => AppState.profileKeep),
      profileVault: computed(() => AppState.profileVault),
      profileVaults: computed(() => AppState.profileVaults)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>
<template>
  <div class="component">

<div class="M-5 ">
  <h1>{{vault}}</h1>
</div>
  </div>
</template>


<script>
import { Vault } from '../models/Vault';
export default {
  name: "Vault",
   props: {
    vault: {
      type: Vault,
      default: () => {
        return new Vault();
      },
    },
  },
  setup(props){
    return {
      vault: computed(() => AppState.vault),
      profile: computed(() => AppState.profile),
      vault: computed(() => AppState.vault),
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
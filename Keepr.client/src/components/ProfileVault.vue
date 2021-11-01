<template>
  <div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">{{profileVault.name}}</h5>
    <p class="card-text">{{profileVault.description}}</p>
        <p>{{profileVault.isPrivate}}</p>

    <a href="#" class="btn btn-primary">Go somewhere</a>
  </div>
</div>
  
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed, onMounted } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from '../utils/Logger';
import { profilesService } from '../services/ProfilesService';
import { Vault } from '../models/Vault';

export default {
  props: {
    profileVault: {
      type: Vault,
      default: () => {
        return new Vault();
      },
    },
  },
  setup(props) {
    // onMounted(async()=>{
    //   await profilesService.getById(route.params.profileId),
    //   await profilesService.getKeepsByProfileId(route.params.profileId),
    //   await profilesService.getVaultsByProfileId(route.params.profileId)
    // })
    return {
      profileVaults: computed(() => AppState.profileVaults),
      vault:computed(() => AppState.vault),
      vaults:computed(() => AppState.vaults)
      }
    }
  }
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

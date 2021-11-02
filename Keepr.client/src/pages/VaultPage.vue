<template>
  <div class="component">

<div class="m-5 masonry-with-columns" >
  <VaultKeep v-for="k in keep" :key="k.id" :keep="k" class="rounded-top m-2"/>
</div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { Vault } from '../models/Vault';
import { AppState } from '../AppState';
import { vaultKeepsService} from '../services/VaultKeepsService';
import Pop from '../utils/Pop';
import { onMounted, watchEffect } from '@vue/runtime-core';
import { logger } from '../utils/Logger';
import { useRoute } from 'vue-router';
import { Keep } from '../models/Keep';
import { profilesService } from '../services/ProfilesService';
export default {
  name: "Vault",
  setup(){
    const route = useRoute();
    onMounted(() => {
      try{
        vaultKeepsService.getById(route.params.vaultId);
        // profilesService.getById(route.params.profileId)
      }catch (error){
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      keep: computed(() => AppState.keep),
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
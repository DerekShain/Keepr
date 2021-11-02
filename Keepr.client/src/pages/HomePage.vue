<template>
  <div class="container-flex">
    <div class="row m-2">
      <div class="masonry-with-columns m-2">
        <Keep v-for="k in keeps" :account="account" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, watchEffect} from "@vue/runtime-core"
import { keepsService } from '../services/KeepsService'
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import { Keep } from '../models/Keep'
import { Account } from '../models/Account'

export default {
  name: 'Home',
  props: {
    keep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
    },
    account: {
      type: Account
    },
  },
  setup(props){
    watchEffect(() => {
      try{
        keepsService.getAll()
      } catch (error){
        Pop.toast(error.message, 'error')
      }
    })
  return{
    keeps: computed(() => AppState.keeps),
    vaults: computed(() => AppState.vaults),
    profile: computed(() => AppState.profile),
    profiles: computed(() => AppState.profiles),
    account: computed(() => AppState.account),
    accounts: computed(() => AppState.accounts),
    vaultkeeps: computed(() => AppState.vaultkeeps),
    vaultkeep: computed(() => AppState.vaultkeep),
    profileVaults: computed(() => AppState.profileVaults),
  }
  }
}
</script>

<style scoped lang="scss">
.home{
  display: grid;
  // height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card{
    width: 50vw;
    > img{
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

// body {
//   margin: 0;
//   padding: 1rem;
// }

// .masonry-with-columns {
//   columns: 6 300px;
//   column-gap: 1rem;
//   div {
//     width: 200px;
//     // background: #EC985A;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     display: inline-block;
//     width: 100%;
//     text-align: center;
//     font-family: system-ui;
//     font-weight: 900;
//     font-size: 2rem;
//   } 
//   @for $i from 1 through 36 { 
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }


// body {
//   margin: 0;
//   padding: 1rem;
// }

.masonry-with-flex {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  max-height: 1000px;
  max-width: 18rem;
  div {
    width: 150px;
    background: #EC985A;
    color: white;
    margin: 0 1rem 1rem 0;
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
.masonry-with-columns {
  columns: 20vw;
  column-gap: 0.5rem;
  div {
    width: 20vw;
    background: #ec985a;
    color: white;
    margin: 2px 1rem 1rem 2px;
    display: inline-block;
    font-family: system-ui;
    font-weight: 500;
    overflow: hidden;
  }
}
</style>

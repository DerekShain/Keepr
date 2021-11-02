<template>
  <div class="">
    <div class="row my-4 mx-2 about">
      <div class="col-2" v-if="profile">
        <img class="rounded user-img" :src="profile.picture" />
      </div>
      <div class="col" v-if="profile">
        <h1>{{ profile.name }}</h1>
        <div class="" v-if="profileVaults">

        <b>Vaults</b> {{profileVaults.length}} 
        </div>
        <div class="" v-if="profileKeeps">

       <b>Keeps</b> {{profileKeeps.length}}
        </div>
      </div>
      <div class="row my-5" v-if="profileVaults">
        <div class="title ">
        <h1>Vaults </h1><i class="mdi mdi-plus f-20" title="Add Vault" data-bs-toggle="modal" :data-bs-target="'#v-modal-' + vault.id" ></i>
        </div>
        <!-- {{profileVaults}} -->
        <ProfileVault v-for="k in profileVaults" :key="k.id" :profileVault="k" class="rounded m-2"/>
      </div>
      <div class="row my-5" v-if="profileKeeps">
        <div class="title">
        <h1>Keeps</h1>
        <i class="mdi mdi-plus f-20" title="Add Keep" data-bs-toggle="modal" :data-bs-target="'#k-modal-' + keep.id" ></i>
        </div>
        <div class="masonry-with-columns">
        <!-- {{profileKeeps}} -->
        <ProfileKeep v-for="k in profileKeeps" :key="k.id" :profileKeep="k" class="rounded m-2"/>
          
        </div>
      </div>
    </div>
  </div>
  <Modal :id="'v-modal-' + vault.id" class="text-light">
    <template #modal-body>
      <VaultForm :vault="vault" class="" />
    </template>
  </Modal>
  <Modal :id="'k-modal-' + keep.id" class="text-light">
    <template #modal-body>
      <KeepForm :keep="keep" class="" />
    </template>
  </Modal>
</template>

<script>
import { computed, watchEffect } from "vue";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { profilesService } from "../services/ProfilesService";
import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService";
import { Account } from "../models/Account";
import { Modal } from 'bootstrap'
export default {
  name: "Profile",
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
    watchEffect(() => {
      profilesService.getById(route.params.profileId),
      profilesService.getKeepsByProfileId(route.params.profileId),
      profilesService.getVaultsByProfileId(route.params.profileId)
    });
    return {
      profile: computed(() => AppState.profile),
      vault: computed(() => AppState.vault),
      keep: computed(() => AppState.keep),
      keeps: computed(() => AppState.keeps),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileKeep: computed(() => AppState.profileKeep),
      profileVault: computed(() => AppState.profileVault),
      profileVaults: computed(() => AppState.profileVaults)
    };
  },
};
</script>

<style lang="scss" scoped>
.user-img {
  height: 90px;
  display: flex;
}
.about{
  display: flex;
  align-items: center;
}
.title{
  display: flex;
  align-items:center;
}
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

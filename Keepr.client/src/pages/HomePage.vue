<template>
  <div class="m-2 row">
    <div class="masonry-with-columns">
      <Keep v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, watchEffect } from "@vue/runtime-core";
import { keepsService } from "../services/KeepsService";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { profilesService } from "../services/ProfilesService";
import { useRoute } from "vue-router";
import { Keep } from "../models/Keep";
import { Account } from "../models/Account";

export default {
  name: "Home",
  props: {
    keep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
    },
    account: {
      type: Account,
    },
  },
  setup(props) {
    watchEffect(() => {
      try {
        keepsService.getAll();
      } catch (error) {
        Pop.toast(error.message, "error");
      }
    });
    return {
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      profiles: computed(() => AppState.profiles),
      vaultkeeps: computed(() => AppState.vaultkeeps),
      vaultkeep: computed(() => AppState.vaultkeep),
      profileVaults: computed(() => AppState.profileVaults),
    };
  },
};
</script>

<style scoped lang="scss">
.home {
  display: grid;
  // height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    background: #ec985a;
    color: white;
    margin: 0 1rem 1rem 0;
    // display: inline-block;
    width: 100%;
    // text-align: center;
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

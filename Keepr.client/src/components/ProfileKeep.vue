<template>
 <div class="mb-2">
  <div class="myCard text-shadow">
    <img :src="profileKeep.img" class="card-img" alt="..." />
    <div
      class="card-img-overlay"
      data-bs-toggle="modal"
      :data-bs-target="'#k-modal-' + profileKeep.id"
      @click="viewCount(profileKeep)"
    >
      <h5 class="card-title">{{ profileKeep.name }}</h5>
      <div class="" v-if="profileKeep.creator">
        <div class="" @click.stop="goToProfile()">
          <img
            :src="profileKeep.creator.picture"
            class="rounded-circle user-img selectable"
            alt=""
          />
        </div>
      </div>
    </div>
  </div>
  </div>
<Modal :id="'k-modal-' + profileKeep.id" class="text-light">
    <template #modal-body>
      <KeepInfo :keep="profileKeep"/>
    </template>
  </Modal>
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed, onMounted, watchEffect } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from '../utils/Logger';
import { profilesService } from '../services/ProfilesService';
import { useRoute } from 'vue-router';
import { Account } from '../models/Account';

export default {
  props: {
    profileKeep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
      account:{
        type: Account
      }
    },
  },
  setup(props) {
    return {
      profileKeeps: computed(() => AppState.profileKeeps),
      keep:computed(() => AppState.keep),
      keeps:computed(() => AppState.keeps),
       async viewCount(profileKeep) {
        try {
          profileKeep.views = profileKeep.views + 1;
          await keepsService.keepInteractions(profileKeep);
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error);
        }
      },
      }
    }
  }

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
  min-width: 0;
  word-wrap: break-word;
  background-clip: border-box;
  border-radius: 35px;
}
.myCard:hover {
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.5);
}

.card-img {
  height: inherit;
  object-fit: cover;
  border-radius: 35px;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
}
.card-img:hover{
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
}
.user-img {
  height: 45px;
}
.text-shadow{
  color: white;
  text-shadow: 2px 2px 4px #000000;
}
</style>

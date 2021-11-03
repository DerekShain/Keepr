<template>
  <div class="myCard m-2 background-colors text-shadow" style="width: 18rem">
    <div class="card-body">
      <h5 class="card-title">{{ profileVault.name }}</h5>
      <p class="card-text">{{ profileVault.description }}</p>
      <div v-if="profileVault.isPrivate === true">
        <b>Private Vault!</b>
      </div>
      <div v-else>
        <b>Public Vault!</b>
      </div>
      <div class="d-flex">
        <router-link
          :to="{ name: 'Vault', params: { vaultId: profileVault.id }, query: {creatorId: account.id} }"
          class="btn btn-black text-light lighten-30 selectable text-uppercase"
        >
          GO TO VAULT
        </router-link>
        <div class="" v-if="profileVault.creatorId === account.id">
          <i
            class="mdi mdi-delete-forever selectable text-danger f-20 mx-3"
            title="Delete Post"
            @click="deleteVault()"
          ></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed, onMounted } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { profilesService } from "../services/ProfilesService";
import { Vault } from "../models/Vault";
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService";
import { Account } from '../models/Account';

export default {
  props: {
    profileVault: {
      type: Vault,
      default: () => {
        return new Vault();
      },
    },
    account:{
      type: Account
    }
  },
  setup(props) {
    const route = useRoute();
    return {
      profileVaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      vaults: computed(() => AppState.vaults),
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(props.profileVault.id);
            Pop.toast("Vault Deleted");
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
.card-img-overlay {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: end;
}
.myCard {
  position: relative;
  background: #fff;
  min-width: 0;
  word-wrap: break-word;
  background-clip: border-box;
  border-radius: 35px;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
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
.background-colors {
	background: linear-gradient(-45deg, #000000, #e67591, #3797bd, #10ad8b);
	background-size: 400% 400%;
	animation: gradient 5s ease infinite;
}

@keyframes gradient {
	0% {
		background-position: 0% 50%;
	}
	50% {
		background-position: 100% 50%;
	}
	100% {
		background-position: 0% 50%;
	}
}
.btn-black {
  background-color: #000;
  color: #fff;
}
.text-shadow{
  text-shadow: 2px 2px 4px #000000;
  color: #fff;
	animation: gradient 15s ease infinite;
}
</style>

<template>
  <form @submit.prevent="createVault()" class="px-5 mx-5 text-dark">
    <div class="form-group">
      <label for="title">Vault Name</label>
      <input type="text"
             class="form-control bg-light"
             name="name"
             placeholder="vault name"
             v-model="editable.name"
      >
    </div>
    <div class="form-group">
      <label for="body">Description</label>
      <input type="text"
             class="form-control bg-light"
             name="title"
             placeholder="description"
             v-model="editable.description"
      >
    </div>
    <div class="form-group">
      <label for="published">Private? </label>
      <input
        type="checkbox"
        class="ms-3 mt-3"
        name="published"
        v-model="editable.isPrivate"
      />
    </div>
      <div class="form-group">
        <button type="submit" class="btn btn-dark mt-2">
          Add Vault
        </button>
      </div>
  </form>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { useRoute } from 'vue-router'
import { Vault } from '../models/Vault'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
export default {
  setup(){
    const editable = ref({})
    const vault = computed(() => AppState.profileVaults)
    const route = useRoute()
    return {
      editable,
      vault,
      async createVault(){
        try{
          await vaultsService.createVault(editable.value)
          editable.value = {}
          Pop.toast('Vault Created', 'success')
          const modal = Modal.getInstance(document.getElementById('v-modal-' + vault.id))
          modal.hide()
        }catch (error){
          Pop.toast('Error Creating Vault', 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>
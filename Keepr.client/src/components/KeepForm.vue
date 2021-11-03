<template>
  <form @submit.prevent="createKeep()" class="px-5 mx-5 text-dark">
    <div class="form-group">
      <label for="title">Keep Name</label>
      <input type="text"
             class="form-control bg-light"
             name="name"
             placeholder="keep name"
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
      <label for="body">Image</label>
      <input type="text"
             class="form-control bg-light"
             name="title"
             placeholder="📷"
             v-model="editable.img"
      >
    </div>
      <div class="form-group">
        <button type="submit" class="btn btn-dark mt-2">
          Add Keep
        </button>
      </div>
  </form>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { useRoute } from 'vue-router'
import { Keep } from '../models/Keep'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
export default {
  setup(){
    const editable = ref({})
    const keep = computed(() => AppState.profileKeeps)
    const route = useRoute()
    return {
      editable,
      keep,
      async createKeep(){
        try{
          await keepsService.createKeep(editable.value)
          editable.value = {}
          Pop.toast('Keep Created', 'success')
          const modal = Modal.getInstance(document.getElementById('k-modal-' + keep.id))
          modal.hide()
        }catch (error){
          Pop.toast('Error Creating Keep', 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>
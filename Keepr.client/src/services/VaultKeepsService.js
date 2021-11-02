import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService{
  async getAll(){
    const res = await api.get('api/keeps')
    logger.log('get all keeps res', res)
    AppState.keeps = res.data.map(k => new VaultKeep(k))
  }
  async getById(vaultId){
    // AppState.keep = null
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('get keep by id res', res)
    AppState.keep = res.data.map(k => new VaultKeep(k))
  }
  async createVaultKeep(keepData){
    const res = await api.post('api/vaultkeeps', keepData)
    logger.log('create keep res', res)
    AppState.vaultkeeps = [new VaultKeep(res.data), ...AppState.vaultkeeps]
  }
  async deleteVaultKeep(vaultKeepId){
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    logger.log('delete keep res', res.data)
    AppState.keep = AppState.keep.filter(k => k.vaultKeepId != vaultKeepId)
  }
}

export const vaultKeepsService = new VaultKeepsService()
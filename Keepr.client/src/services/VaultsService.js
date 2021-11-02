import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
  async getAll(){
    const res = await api.get('api/vaults')
    logger.log('get all vaults res', res)
    AppState.vaults = res.data.vault(k => new Vault(k))
  }
  async getById(vaultId){
    AppState.vault = null
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log('get vault by id res', res)
    AppState.vault = new Vault(res.data)
  }
  async createVault(vaultData){
    logger.log('create vault res', vaultData)
    // vaultData.isPrivate = false
    const res = await api.post('api/vaults', vaultData)
    logger.log('create vault res', res)
    // AppState.vaults = [new Vault(res.data), ...AppState.vaults]
  }
  async editVault(vaultData){
    const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
    logger.log('edit vault res', res)
    let vaultIndex = AppState.vaults.findIndex(k => k.id == vaultData.id)
    AppState.vaults.splice(vaultIndex, 1, new Vault(res.data))
  }
  async deleteVault(vaultId){
    logger.log('vault vaultId', vaultId)
    const res = await api.delete(`api/vaults/${vaultId}`)
    logger.log('delete vault res', res.data)
    AppState.vaults = AppState.vaults.filter(k => k.id !== vaultId)
  }
}

export const vaultsService = new VaultsService()
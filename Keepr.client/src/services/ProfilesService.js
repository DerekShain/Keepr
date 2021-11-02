import { AppState } from "../AppState"
import { Profile } from "../models/Profile"
import { Keep } from "../models/Keep"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService{
  
  async getById(profileId){
    AppState.profile = null
    const res = await api.get(`api/profiles/${profileId}`)
    logger.log('get profile by id res', res)
    AppState.profile = new Profile(res.data)
  }
  async getKeepsByProfileId(profileId, keepData){
    AppState.profileKeeps = null
    const res = await api.get(`api/profiles/${profileId}/keeps`, keepData)
    logger.log('get profile keeps res', res)
    AppState.profileKeeps = res.data.map(k => new Keep(k))

  }
  async getVaultsByAccountId(profileId, vaultData){
    AppState.accountVaults = null
    const res = await api.get(`api/profiles/${profileId}/vaults`, vaultData)
    logger.log('get profile vaults res', res)
    AppState.accountVaults = res.data.map(k => new Vault(k))
  }
  async getVaultsByProfileId(profileId, vaultData){
    AppState.profileVaults = null
    const res = await api.get(`api/profiles/${profileId}/vaults`, vaultData)
    logger.log('get profile vaults res', res)
    AppState.profileVaults = res.data.map(k => new Vault(k))
  }
}

export const profilesService = new ProfilesService()
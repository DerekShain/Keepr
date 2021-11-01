import { AppState } from "../AppState"
import { Profile } from "../models/Profile"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService{
  
  async getById(profileId){
    AppState.profile = null
    const res = await api.get(`api/profiles/${profileId}`)
    logger.log('get profile by id res', res)
    AppState.profile = new Profile(res.data)
  }
  async getKeepsByProfileId(profileId){
    AppState.profile = null
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log('get profile keeps res', res)
    AppState.profile = new Profile(res.data)
  }
  async getVaultsByProfileId(profileId){
    AppState.profile = null
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log('get profile vaults res', res)
    AppState.profile = new Profile(res.data)
  }
}

export const profilesService = new ProfilesService()
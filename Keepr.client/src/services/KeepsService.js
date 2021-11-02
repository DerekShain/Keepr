import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
  async getAll(){
    const res = await api.get('api/keeps')
    // logger.log('get all keeps res', res)
    AppState.keeps = res.data.map(k => new Keep(k))
  }
  async getById(keepId){
    AppState.keep = null
    const res = await api.get(`api/keeps/${keepId}`)
    // logger.log('get keep by id res', res)
    AppState.keep = new Keep(res.data)
  }
  async createKeep(keepData){
    logger.log('create keep res', keepData)
    const res = await api.post('api/keeps', keepData)
    logger.log('create keep res', res)
    AppState.keeps = [new Keep(res.data), ...AppState.keeps]
  }
  async editKeep(keepData){
    const res = await api.put(`api/keeps/${keepData.id}`, keepData)
    logger.log('edit keep res', res)
    let keepIndex = AppState.keeps.findIndex(k => k.id == keepData.id)
    AppState.keeps.splice(keepIndex, 1, new Keep(res.data))
  }
  async keepInteractions(keepData){
    const res = await api.put(`api/keeps/${keepData.id}/interaction`, keepData)
    logger.log('edit keep res', res)
    let keepIndex = AppState.keeps.findIndex(k => k.id == keepData.id)
    AppState.keeps.splice(keepIndex, 1, new Keep(res.data))
  }
  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log('delete keep res', res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id !== keepId)
  }
}

export const keepsService = new KeepsService()
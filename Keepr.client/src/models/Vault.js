export class Vault
{
  constructor (data){
    this.id = data.id
    this.name = data.name
    this.description = data.description
    this.creatorId = data.creatorId
    this.isPrivate = data.isPrivate
    this.creator = data.creator || {}
  }
}
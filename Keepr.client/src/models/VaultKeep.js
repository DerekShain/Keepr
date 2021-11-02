export class VaultKeep
{
  constructor (data= {}){
    this.id = data.id
    this.creatorId = data.creatorId
    this.profileId = data.profileId 
    this.accountId = data.accountId
    this.vaultKeepId = data.vaultKeepId
    this.keepId = data.keepId
    this.name = data.name
    this.img = data.img
    this.views = data.views
  }
}
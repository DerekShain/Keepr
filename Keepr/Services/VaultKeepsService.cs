using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _rs;
    private readonly VaultsRepository _vrs;
    public VaultKeepsService(VaultKeepsRepository rs, VaultsRepository vrs)
    {
      _rs = rs;
      _vrs = vrs;
    }
    public VaultKeep Create(VaultKeep vk)
    {
      return _rs.Create(vk);
    }

    public List<VaultKeepViewModel> Get(Vault vault)
    {
      List<VaultKeepViewModel> vaultKeeps = new List<VaultKeepViewModel>();
      if (vault.IsPrivate == true)
      {
        vaultKeeps = _rs.GetPrivateKeepsById(vault.Id);
      }
      else
      {
        vaultKeeps = _rs.GetKeepsById(vault.Id);
      }
      return vaultKeeps;

    }

    internal void Delete(int keepVaultId, string userId)
    {
      VaultKeep exists = GetById(keepVaultId);
      if (exists.CreatorId != userId)
      {
        throw new Exception("Invalid");
      }
      _rs.Delete(keepVaultId);
    }

    private VaultKeep GetById(int keepVaultId)
    {
      return _rs.GetAll(keepVaultId);
    }
  }
}
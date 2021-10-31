using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _rs;
    public VaultsService(VaultsRepository rs)
    {
      _rs = rs;
    }
    public List<Vault> GetAll()
    {
      return _rs.Get();
    }

    public Vault GetById(int vaultId, string userId)
    {
      Vault Exists = _rs.GetById(vaultId);
      if (Exists == null)
      {
        throw new Exception("Invalid Id");
      }
      if (Exists.IsPrivate == true && Exists.CreatorId != userId)
      {
        throw new Exception("You do not have access to this vault");
      }
      return Exists;
    }

    public Vault GetVaultById(int vaultId, string userId)
    {
      Vault Exists = _rs.GetById(vaultId);
      if (Exists == null)
      {
        throw new Exception("Invalid Id");
      }
      if (Exists.IsPrivate == false && Exists.CreatorId != userId)
      {
        throw new Exception("You do not have access to this vault");
      }
      return Exists;
    }

    public Vault Create(Vault vaultData)
    {
      return _rs.Create(vaultData);
    }

    public Vault Edit(int vaultId, Vault vaultData, string userId)
    {
      var vault = GetById(vaultId, userId);
      if (vault.CreatorId != vaultData.CreatorId)
      {
        throw new Exception("Invalid Id");
      }
      vault.Name = vaultData.Name ?? vault.Name;
      vault.Description = vaultData.Description ?? vault.Description;
      vault.CreatorId = vaultData.CreatorId ?? vault.CreatorId;
      vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;
      return _rs.Edit(vault);
    }

    public List<Vault> GetVaultByProfile(string profileId, string userId)
    {
      if (profileId == userId)
      {
        return _rs.GetVaultByProfile(profileId, userId);
      }
      return _rs.GetVaultByProfile(profileId, userId);
    }

    public void Delete(int vaultId, string userId)
    {
      Vault exists = GetById(vaultId, userId);
      if (exists.CreatorId != userId)
      {
        throw new Exception("Invalid Id");
      }
      _rs.Delete(vaultId);
    }
  }
}
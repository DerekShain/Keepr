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

    public Vault GetById(int vaultId)
    {
      Vault Exists = _rs.GetById(vaultId);
      if (Exists == null)
      {
        throw new Exception("Invalid Id");
      }
      return Exists;
    }

    public Vault Create(Vault vaultData)
    {
      return _rs.Create(vaultData);
    }

    public object Edit(Vault vaultData)
    {
      Vault vault = GetById(vaultData.Id);
      if (vault.CreatorId != vaultData.CreatorId)
      {
        throw new Exception("Invalid Id");
      }
      vault.Name = vaultData.Name ?? vault.Name;
      vault.Description = vaultData.Description ?? vault.Description;
      vault.CreatorId = vaultData.CreatorId ?? vault.CreatorId;
      vault.IsPrivate = vaultData.IsPrivate;
      return _rs.Edit(vault);
    }

    public void Delete(int vaultId, string id)
    {
      Vault exists = GetById(vaultId);
      if (exists.CreatorId != id)
      {
        throw new Exception("Invalid Id");
      }
      _rs.Delete(vaultId);
    }
  }
}
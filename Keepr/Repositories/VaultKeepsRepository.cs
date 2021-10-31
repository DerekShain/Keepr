using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    public VaultKeep Create(VaultKeep vk)
    {
      string sql = @"
      INSERT INTO
     vault_keeps(vaultId, keepId, creatorId)
     VALUES (@VaultId, @KeepId, @CreatorId);
     SELECT LAST_INSERT_ID();
     ";
      vk.Id = _db.ExecuteScalar<int>(sql, vk);
      return vk;
    }

    internal List<VaultKeepViewModel> GetPrivateKeepsById(int vaultId)
    {
      var sql = @"
      SELECT 
      vk.id as vaultKeepId,
      vk.keepId as keepId,
      v.*,
      k.*
      FROM vault_keeps vk
      JOIN accounts a on a.id = vk.creatorId
      JOIN vaults v on v.id = @vaultId
      JOIN keeps k on k.id = vk.keepId
      WHERE vk.vaultId = @vaultId
      ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
      // return _db.Query<Review, Profile, Review>(sql, (r, a) =>
      // {
      //   r.Creator = a;
      //   return r;
      // }, new { id }).ToList();
    }

    internal List<VaultKeepViewModel> GetKeepsById(int vaultId)
    {
      var sql = @"
      SELECT 
      vk.id as vaultKeepId,
      vk.keepId as keepId,
      v.*,
      k.*
      FROM vault_keeps vk
      JOIN accounts a on a.id = vk.creatorId
      JOIN vaults v on v.id = @vaultId
      JOIN keeps k on k.id = vk.keepId
      WHERE vk.vaultId = @vaultId
      ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
      // return _db.Query<Review, Profile, Review>(sql, (r, a) =>
      // {
      //   r.Creator = a;
      //   return r;
      // }, new { id }).ToList();
    }
    internal VaultKeep GetAll(int keepVaultId)
    {
      var sql = @"
      SELECT
      *
      FROM vault_keeps
      WHERE id = @keepVaultId LIMIT 1;";
      return _db.Query<VaultKeep>(sql, new { keepVaultId }).FirstOrDefault();
    }

    internal void Delete(int keepVaultId)
    {
      var sql = @"
      DELETE
      FROM vault_keeps
      WHERE id = @keepVaultId LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, new { keepVaultId });
      if (rowsAffected == 0)
      {
        throw new Exception("none affected");
      }
      if (rowsAffected > 1)
      {
        throw new Exception("Too Many");
      }
    }
  }
}
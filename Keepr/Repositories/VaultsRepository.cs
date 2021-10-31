using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    public List<Vault> Get()
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }
      ).ToList();
    }

    public Vault GetById(int vaultId)
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @vaultId;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { vaultId }).FirstOrDefault();
    }

    public Vault Create(Vault vaultData)
    {
      var sql = @"
      INSERT INTO vaults(
        name,
        description,
        creatorId,
        isPrivate
      )
      VALUES(
        @Name,
        @Description,
        @CreatorId,
        @IsPrivate
      );
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal List<Vault> GetVaultByProfile(string profileId, string userId)
    {
      string sql = @"
      SELECT
      *
      FROM vaults v
      WHERE v.creatorId = @profileId
      ";
      return _db.Query<Vault>(sql, new { profileId }).ToList();
    }

    public Vault Edit(Vault vaultData)
    {
      string sql = @"
      UPDATE vaults
      SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate
      WHERE id = @Id;
      ";
      var rowsAffected = _db.Execute(sql, vaultData);
      if (rowsAffected == 0)
      {
        throw new Exception("No rows affected");
      }
      if (rowsAffected > 1)
      {
        throw new Exception("Too many rows affected");
      }
      return vaultData;
    }

    internal void Delete(int vaultId)
    {
      var sql = @"
      DELETE
      FROM vaults
      WHERE id = @vaultId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new { vaultId });
      if (rowsAffected == 0)
      {
        throw new Exception("No rows affected");
      }
      if (rowsAffected > 1)
      {
        throw new Exception("Too many rows affected");
      }

    }
  }
}
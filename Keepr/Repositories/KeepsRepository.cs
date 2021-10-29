using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Keep> Get()
    {
      string sql = @"
      SELECT
      *
      FROM keeps
      ";
      return _db.Query<Keep>(sql).ToList();
    }

    public Keep GetById(int id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id;";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }

    public Keep Create(Keep keepData)
    {
      var sql = @"
      INSERT INTO keeps(
        name,
        description,
        img,
        views,
        shares,
        keeps,
        creatorId
      ) 
      VALUES(
        @Name,
        @Description,
        @Img,
        @Views,
        @Shares,
        @Keeps,
        @CreatorId
      );
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    public Keep Edit(Keep keepData)
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps
      WHERE id = @Id;";
      var rowsAffected = _db.Execute(sql, keepData);
      if (rowsAffected == 0)
      {
        throw new Exception("No rows affected");
      }
      if (rowsAffected > 1)
      {
        throw new Exception("Too many rows affected");
      }
      return keepData;
    }

    internal void Delete(int keepId)
    {
      var sql = @"
      DELETE
      FROM keeps
      WHERE id = @keepId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new { keepId });
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
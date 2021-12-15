// WHERE THE SQL GOES TODO
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using knightsAndCastles.Models;

namespace knightsAndCastles.Repositories
{
  public class CastleRepository
  {
    private readonly IDbConnection _db;

    public CastleRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Castle> GetAll()
    {
      string sql = @"
      SELECT
        castle.*,
        account.*
        FROM castles castle
        JOIN accounts account ON castle.creatorId = account.id
      ;";

      return _db.Query<Castle, Account, Castle>(sql, (castle, account) =>
      {
        castle.Creator = account;
        return castle;
      }, splitOn: "Id").ToList();
    }

    internal Castle GetById(int id)
    {
      string sql = "SELECT * FROM castles WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Castle>(sql, new { id });
    }

    internal Castle Create(Castle newCastle)
    {
      string sql = @"
  INSERT INTO castles
  (defense, name, creatorId)
  VALUES
  (@Defense, @Name, @CreatorId);
  SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newCastle);
      newCastle.Id = id;
      return newCastle;
    }

    internal void Remove(int id)
    {
      string sql = @"
  DELETE FROM castles
  WHERE ID = @Id
  ;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("invalid id");
      }
    }

    internal Castle Update(Castle updatedCastle)
    {
      string sql = @"
      UPDATE castles
      SET
      name = @Name, 
      defense = @Defense
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedCastle);
      if (rows <= 0)
      {
        throw new Exception("Car update failed");
      }
      return updatedCastle;
    }

  }
}
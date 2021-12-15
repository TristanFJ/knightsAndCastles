using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using knightsAndCastles.Models;

namespace knightsAndCastles.Repositories
{
  public class KnightRepository
  {
    private readonly IDbConnection _db;
    public KnightRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Knight> GetAll()
    {
      string sql = @"
  SELECT
    knight.*,
    castle.*
    FROM knights knight
    JOIN castles castle ON knight.creatorId = account.id
  ;";
      return _db.Query<Knight, Castle, Knight>(sql, (knight, castle) =>
      {
        knight.CastleId = castle.Id;
        return knight;
      }, splitOn: "id").ToList();
    }

    internal Knight GetById(int id)
    {
      string sql = "SELECT * FROM knight WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Knight>(sql, new { id });
    }

    internal Knight Create(Knight newKnight)
    {
      string sql = @"
      INSERT INTO cars
      (weapon, age, surname, castleId, castle)
      VALUES
      (@Weapon, @Age, @Surname, @CastleId, @Castle);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newKnight);
      newKnight.Id = id;
      return newKnight;
    }

  }
}
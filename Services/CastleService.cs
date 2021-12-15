using System;
using System.Collections.Generic;
using knightsAndCastles.Models;
using knightsAndCastles.Repositories;

namespace knightsAndCastles.Services
{
  public class CastleService
  {
    private readonly CastleRepository _repo;

    public CastleService(CastleRepository repo)
    {
      _repo = repo;
    }
    internal List<Castle> Get()
    {
      return _repo.GetAll();
    }

    internal Castle Get(int id)
    {
      Castle found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Castle Create(Castle newCastle)
    {
      _repo.Create(newCastle);
      return newCastle;
    }

    internal Castle Update(Castle updatedCastle)
    {
      Castle oldCastle = Get(updatedCastle.Id);
      updatedCastle.Defense = updatedCastle.Defense != 0 ? updatedCastle.Defense : oldCastle.Defense;
      updatedCastle.Name = updatedCastle.Name != null ? updatedCastle.Name : oldCastle.Name;
      updatedCastle.Creator = updatedCastle.Creator != null ? updatedCastle.Creator : oldCastle.Creator;
      updatedCastle.CreatorId = updatedCastle.CreatorId != null ? updatedCastle.CreatorId : oldCastle.CreatorId;
      return _repo.Update(updatedCastle);
    }

    internal void Remove(int id)
    {
      Castle castle = Get(id);
      _repo.Remove(id);
    }

  }
}
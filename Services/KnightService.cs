using System;
using System.Collections.Generic;
using knightsAndCastles.Models;
using knightsAndCastles.Repositories;
namespace knightsAndCastles.Services
{
  public class KnightService
  {
    private readonly KnightRepository _repo;

    public KnightService(KnightRepository repo)
    {
      _repo = repo;
    }

    internal List<Knight> Get()
    {
      return _repo.GetAll();
    }

    internal Knight Get(int id)
    {
      Knight found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("invalid id");
      }
      return found;
    }

    internal Knight Create(Knight newKnight)
    {
      _repo.Create(newKnight);
      return newKnight;
    }


  }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using knightsAndCastles.Models;
using knightsAndCastles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace knightsAndCastles.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CastleController : ControllerBase
  {
    private readonly CastleService _cs;
    public CastleController(CastleService cs)
    {
      _cs = cs;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Castle>> Get()
    {
      try
      {
        List<Castle> castles = _cs.Get();
        return Ok(castles);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Castle>> Create([FromBody] Castle newCastle)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        newCastle.CreatorId = user.Id;
        Castle castle = _cs.Create(newCastle);
        castle.Creator = user;
        return Ok(castle);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Castle> Create([FromBody] Castle updatedCastle, int id)
    {
      try
      {
        updatedCastle.Id = id;
        Castle castle = _cs.Update(updatedCastle);
        return Ok(castle);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<String> Remove(int id)
    {
      try
      {
        _cs.Remove(id);
        return Ok("Castle removed");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}
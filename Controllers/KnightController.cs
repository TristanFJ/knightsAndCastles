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
  public class KnightController : ControllerBase
  {
    private readonly KnightService _ks;
    public KnightController(KnightService ks)
    {
      _ks = ks;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Knight>> Get()
    {
      try
      {
        List<Knight> knights = _ks.Get();
        return Ok(knights);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [HttpPost]
    // [Authorize]
    // public Task<ActionResult<Knight>> Create([FromBody] Knight newKnight)
    // {
    //   try
    //   {


    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
  }
}
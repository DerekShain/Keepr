using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  // [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }


    // GET api/keeps
    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        var keeps = _ks.GetAll();
        return Ok(keeps);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // GET api/keeps/{id}
    [HttpGet("{keepId}")]
    public ActionResult<Keep> Get(int keepId)
    {
      try
      {
        var keeps = _ks.GetById(keepId);
        return Ok(keeps);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // POST api/keeps
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Post([FromBody] Keep keepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepData.CreatorId = userInfo.Id;
        Keep createdKeep = _ks.Create(keepData);
        createdKeep.Creator = userInfo;
        return Ok(createdKeep);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // PUT api/keeps/{id}
    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int keepId, [FromBody] Keep keepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepData.CreatorId = userInfo.Id;
        keepData.Id = keepId;
        return Ok(_ks.Edit(keepData));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // DELETE api/keeps/{id}
    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<string>> DELETE(int keepId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ks.Delete(keepId, userInfo.Id);
        return Ok("Keep Deleted!");
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }
    [HttpGet]
    public ActionResult<List<Vault>> GetAll()
    {
      try
      {
        var vaults = _vs.GetAll();
        return Ok(vaults);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetAsync(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        var vault = _vs.GetById(vaultId, userInfo?.Id);
        return Ok(vault);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        Vault createdVault = _vs.Create(vaultData);
        createdVault.Creator = userInfo;
        return Ok(createdVault);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int vaultId, [FromBody] Vault vaultData, string userId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        userId = userInfo.Id;
        vaultData.Id = vaultId;
        Vault vault = _vs.Edit(vaultId, vaultData, userId);
        return Ok(vault);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vs.Delete(vaultId, userInfo.Id);
        return Ok("VAULT has been Deleted!");
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }

}

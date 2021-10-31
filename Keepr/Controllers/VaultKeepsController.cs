using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

  // POST VaultKeep
  // GET VaultKeeps
  // DELETE VaultKeeps
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks, VaultsService vs)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vk)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault userVault = _vs.GetVaultById(vk.VaultId, userInfo.Id);

        vk.CreatorId = userInfo.Id;
        VaultKeep newKeep = _vks.Create(vk);
        return Ok(newKeep);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}
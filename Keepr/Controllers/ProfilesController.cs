using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;
    public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
    {
      _ps = ps;
      _ks = ks;
      _vs = vs;
    }
    [HttpGet("{profileId}")]
    public ActionResult<Profile> Get(string profileId)
    {
      try
      {
        Profile profile = _ps.Get(profileId);
        return Ok(profile);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string profileId)
    {
      try
      {
        var profile = _ks.GetKeepByProfile(profileId);
        return Ok(profile);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsAsync(string profileId)
    {
      try
      {
        List<Vault> vaults = new List<Vault>();
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (profileId == userInfo?.Id)
        {
          vaults = _vs.GetVaultByProfile(profileId, userInfo?.Id);
        }
        else
        {
          vaults = _vs.GetVaultByProfile(profileId, userInfo?.Id).FindAll(v => v.IsPrivate == false);
        }
        return Ok(vaults);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }

}
using System.Collections.Generic;
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
    public ActionResult<List<Vault>> GetVaults(string profileId)
    {
      try
      {
        var profile = _vs.GetVaultByProfile(profileId);
        return Ok(profile);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }

}
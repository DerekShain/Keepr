using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _rs;
    public ProfilesService(ProfilesRepository rs)
    {
      _rs = rs;
    }
    public Profile Get(string profileId)
    {
      Profile profile = _rs.Get(profileId);
      if (profile == null)
      {
        throw new Exception("Invalid Id");
      }
      return profile;
    }
  }
}
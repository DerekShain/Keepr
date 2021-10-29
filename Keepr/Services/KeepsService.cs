using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _rs;
    public KeepsService(KeepsRepository rs)
    {
      _rs = rs;
    }
    public List<Keep> GetAll()
    {
      return _rs.Get();
    }
    public Keep GetById(int keepId)
    {
      Keep exists = _rs.GetById(keepId);
      if (exists == null)
      {
        throw new Exception("Invalid Id");
      }
      return exists;
    }
    public Keep Create(Keep keepData)
    {
      return _rs.Create(keepData);
    }
    public Keep Edit(Keep keepData)
    {
      Keep keep = GetById(keepData.Id);
      if (keep.CreatorId != keepData.CreatorId)
      {
        throw new Exception("Invalid Id");
      }
      keep.Name = keepData.Name ?? keep.Name;
      keep.Description = keepData.Description ?? keep.Description;
      keep.Img = keepData.Img ?? keep.Img;
      keep.Views = keepData.Views;
      keep.Shares = keepData.Shares;
      keep.Keeps = keepData.Keeps;
      return _rs.Edit(keep);
    }
    public void Delete(int keepId, string userId)
    {
      Keep exists = GetById(keepId);
      if (exists.CreatorId != userId)
      {
        throw new Exception("This is not your keep");
      }
      _rs.Delete(keepId);
    }
  }
}
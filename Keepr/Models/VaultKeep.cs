namespace Keepr.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public string CreatorId { get; set; }
  }
  public class VaultKeepViewModel : Keep
  {
    public int KeepId { get; set; }
    public int VaultKeepId { get; set; }
  }
}
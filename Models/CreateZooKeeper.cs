using System.Text.Json.Serialization;

namespace ZooManagement;

public class CreateZooKeeper
{
  [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]

  public int ZooKeeperID { get; set; }
  public string? ZooKeeperName { get; set; }
  public string? EnclosureName { get; set; }

}
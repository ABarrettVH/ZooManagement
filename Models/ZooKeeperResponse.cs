using System.Text.Json.Serialization;

namespace ZooManagement;

public class ZooKeeperResponse
{
  [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]

  public int ZooKeeperID { get; set; }
  public List<string>? EnclosureName { get; set; }
  public List<int>? Animals { get; set; }

}
using System.Text.Json.Serialization;

namespace ZooManagement;

public class ZooKeeper
{


    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    public int ZooKeeperID { get; set; }

    public string? ZooKeeperName { get; set; }

    //  public int EnclosureID { get; set; }
    public string? EnclosureName {get; set;}

    [JsonIgnore]
    public IList<Enclosure>? Enclosures { get; set; }
     
     




}
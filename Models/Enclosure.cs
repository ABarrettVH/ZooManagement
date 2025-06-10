namespace ZooManagement;

public class Enclosure
{
   

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    public int EnclosureID { get; set; }
    //  public int AnimalId { get; set; }
    public string? EnclosureName { get; set; }
    public IList<Animal>? Animals { get; set; }
   
    public int ZooKeeperID { get; set; }
    // public IList<ZooKeeper>? ZooKeepers {get; set;}

}
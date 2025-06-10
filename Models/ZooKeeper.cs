namespace ZooManagement;

public class ZooKeeper
{


    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    public int ZooKeeperID { get; set; }

     public string? ZooKeeperName { get; set; }
     
    //  public int EnclosureID { get; set; }

     public IList<Enclosure>? Enclosures { get; set; }



}
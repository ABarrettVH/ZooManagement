namespace ZooManagement;

public class EnclosureZooKeeper
{
    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    public int EnclosureZooKeeperID { get; set; }

    public int ZooKeeperID { get; set; }

    // public ZooKeeper? ZooKeeper { get; set; }
    public int EnclosureID { get; set; }
    // public Enclosure? Enclosure { get; set; }
    



}
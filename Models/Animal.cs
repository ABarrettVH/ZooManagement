namespace ZooManagement;

public class Animal
{
    //    public Animal()
    // {
    //     Enclosures = new List<Enclosure>();
    // }

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    public int AnimalId { get; set; }
    public string? Species { get; set; }
    public string? Classification { get; set; }
    public string? Name { get; set; }
    public string? Sex { get; set; }

    public string? DOB { get; set; }
    public string? ArrivedAtZoo { get; set; }

    public int Age { get; set; }

   
    public int EnclosureID { get; set; }
    

    public Enclosure enclosure { get; set; }
    
    
}
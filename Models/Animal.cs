using System.Text.Json.Serialization;

namespace ZooManagement;

public class Animal
{
    //    public Animal()
    // {
    //     Enclosures = new List<Enclosure>();
    // }

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int AnimalId { get; set; }
    public string? Species { get; set; }
    
    [JsonIgnore] 
    public string? Classification { get; set; }
    public string? Name { get; set; }
    public string? Sex { get; set; }

    public string? DOB { get; set; }
    public string? ArrivedAtZoo { get; set; }

   [JsonIgnore] 
    public int Age { get; set; }

  [JsonIgnore] 
    public int EnclosureID { get; set; }
    
    // [JsonIgnore]
    // public Enclosure enclosure { get; set; }
    
    
}
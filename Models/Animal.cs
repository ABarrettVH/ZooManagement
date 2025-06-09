namespace ZooManagement;

public class Animal
{

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Species { get; set; }
    public string? Classification { get; set; }
    public string? Name { get; set; }
    public string? Sex { get; set; }

    public string? DOB { get; set; }
    public string? ArrivedAtZoo { get; set; } 
    
    
}
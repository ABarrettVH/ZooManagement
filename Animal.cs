namespace ZooManagement;

public class Animal
{
    public int AnimalID { get; set; }
    public string? Species { get; set; }
    public string? Classification { get; set; }
    public string? Name { get; set; }
    public int Sex { get; set; }
    public DateOnly DOB { get; set; }
    public DateOnly ArrivedAtZoo { get; set; } 
}
namespace ZooManagement.Models
{
     public class PaginationParams
{
    
    public int PageNumber { get; set; }

     public int PageSize {get; set;}
     public string? SearchTerm { get; set; }


     public string? orderByCategory {get; set;}
}
}
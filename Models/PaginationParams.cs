namespace ZooManagement.Models
{
     public class PaginationParams
{
    // private const int MaxPageSize = 20;
    public int PageNumber { get; set; }

    private int _pageSize = 10;
    public int PageSize {get; set;}
     public string? SearchTerm { get; set; }


     public string? orderByCategory {get; set;}
}
}
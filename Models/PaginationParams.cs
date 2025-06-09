namespace ZooManagement.Models
{
     public class PaginationParams
{
    // private const int MaxPageSize = 20;
    public int PageNumber { get; set; }

    private int _pageSize = 10;
    public int PageSize {get; set;}
    // {
    //     get => _pageSize;
    //     set => _pageSize;


    //     // set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    // }
}
}
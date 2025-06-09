

namespace ZooManagement.Models
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalAnimalRecords { get; set; }

        public PagedResponse(List<T> data, int pageNumber, int pageSize, int totalAnimalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalAnimalRecords = totalAnimalRecords;
        }
    }
}
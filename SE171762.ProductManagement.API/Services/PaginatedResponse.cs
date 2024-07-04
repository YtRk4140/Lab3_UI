using System.Text.Json.Serialization;

namespace SE171762.ProductManagement.API.Services
{
    public class PaginatedResponse<T>
    {
        [JsonPropertyName("total-pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("page-size")]
        public int PageSize { get; set; }
        [JsonPropertyName("page-index")]
        public int PageIndex { get; set; }
        [JsonPropertyName("total-items")]
        public int TotalItems { get; set; }
        [JsonPropertyName("page-items")]
        public int PageItems { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PaginatedResponse(int totalPages, int pageSize, int pageIndex, int totalItems,
            int pageItems, IEnumerable<T> items) 
        {
            TotalPages = totalPages;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalItems = totalItems;
            PageItems = pageItems;
            Items = items;
        }
    }
}

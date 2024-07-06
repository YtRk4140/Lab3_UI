using SE171762.ProductManagement.API.Services.Product;
using System.Text.Json.Serialization;

namespace SE171762.ProductManagement.API.Services
{
    public class PaginatedResponse
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
        public IEnumerable<ProductResponse> Items { get; set; }

    }
}

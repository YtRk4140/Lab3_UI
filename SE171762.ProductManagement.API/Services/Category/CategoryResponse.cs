using SE171762.ProductManagement.API.Services.Product;
using System.Text.Json.Serialization;

namespace SE171762.ProductManagement.API.Services.Category
{
    public class CategoryResponse
    {
        [JsonPropertyName("category-id")]
        public int CategoryId { get; set; }
        [JsonPropertyName("category-name")]
        public string CategoryName { get; set; }
        public IEnumerable<ProductRequest> Products { get; set; }
    }
}

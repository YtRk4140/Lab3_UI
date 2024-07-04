using System.Text.Json.Serialization;

namespace SE171762.ProductManagement.API.Services.Category
{
    public class CategoryRequest
    {
        [JsonPropertyName("category-id")]
        public int CategoryId { get; set; }
        [JsonPropertyName("category-name")]
        public string CategoryName { get; set; }
    }
}

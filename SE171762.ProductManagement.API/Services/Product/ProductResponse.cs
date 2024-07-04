using NET171462.ProductManagement.Repo.Models;
using System.Text.Json.Serialization;

namespace SE171762.ProductManagement.API.Services.Product
{
    public class ProductResponse
    {
        [JsonPropertyName("category-id")]
        public int CategoryId { get; set; }

        [JsonPropertyName("category-name")]
        public string CategoryName { get; set; }
        
        [JsonPropertyName("product-id")]
        public int ProductId { get; set; }
        
        [JsonPropertyName("product-name")]
        public string ProductName { get; set; }
        
        [JsonPropertyName("unit-in-stock")]
        public short UnitsInStock { get; set; }
        
        [JsonPropertyName("unit-price")]
        public decimal UnitPrice { get; set; }
    }
}

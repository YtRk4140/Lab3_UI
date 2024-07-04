using SE171762.ProductManagement.API.Services.Product;
using System.Text.Json.Serialization;

namespace SE171762.ProductManagement.API.Services.Account
{
    public class AccountResponse
    {
        [JsonPropertyName("member-id")]
        public string MemberId { get; set; }

        [JsonPropertyName("full-name")]
        public string FullName { get; set; }

        [JsonPropertyName("email-address")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("member-password")]
        public string MemberPassword { get; set; }

        [JsonPropertyName("member-role")]
        public int MemberRole { get; set; }
    }
}

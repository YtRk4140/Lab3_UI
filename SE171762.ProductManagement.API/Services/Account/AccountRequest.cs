using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SE171762.ProductManagement.API.Services.Account
{
    public class AccountRequest
    {
        public record RruAccount(
            [Required(ErrorMessage = "{0} is required")]
            [EmailAddress(ErrorMessage = "{0} should be a proper email.")]
            string EmailAddress,

            [Required(ErrorMessage = "{0} is required")]
            string FullName,

            [Required(ErrorMessage = "{0} is required")]
            string MemberPassword
        );

        public record Login(
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage ="{0} should be a proper email.")]
            string EmailAddress,

            [Required(ErrorMessage = "Password is required")]
            string MemberPassword
        );

        public record CreateAcc(
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage ="{0} should be a proper email.")]
            string EmailAddress,

            [Required(ErrorMessage = "Password is required")]
            string MemberPassword,

            [Required(ErrorMessage = "Member Role is required")]
            int MemberRole
        );

    }
}

using Microsoft.AspNetCore.Mvc;
using NET171462.ProductManagement.Repo.Repository.Interface;
using SE171462.ProductManagement.Repo.JwtService;
using SE171462.ProductManagement.Repo.JwtService.Interface;
using SE171762.ProductManagement.API.Services.Account;
using System.Security.Claims;

namespace SE171762.ProductManagement.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IJwtTokenService jwtTokenService;
        private readonly IUnitOfWork unitOfWork;

        public AuthenController(
            IJwtTokenService jwtTokenService,
            IUnitOfWork unitOfWork)
        {
            this.jwtTokenService = jwtTokenService;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] AccountRequest.Login request)
        {
            var user = await unitOfWork.AccountRepository.FindByEmailAsync(request.EmailAddress);
            if (user != null && user.MemberPassword == request.MemberPassword)
            {

                var role = unitOfWork.AccountRepository.GetByID(user.MemberId);
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.EmailAddress),
                        new Claim(ClaimTypes.Role, role.MemberId),
                    };

                // GetToken
                var token = jwtTokenService.GenerateAccessToken(claims);
                return Ok(token);
            }

            return BadRequest("Invalid email or password.");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SE171462.ProductManagement.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        public AuthenticationController(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}

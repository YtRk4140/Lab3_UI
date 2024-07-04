using Microsoft.AspNetCore.Mvc;

namespace SE171462.ProductManagement.UI.Controllers
{
    public class ProductController_ : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

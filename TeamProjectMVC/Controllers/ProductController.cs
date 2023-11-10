using Microsoft.AspNetCore.Mvc;

namespace TeamProjectMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }
    }
}

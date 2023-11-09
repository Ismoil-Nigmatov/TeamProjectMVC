using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Repository;

namespace TeamProjectMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly  IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [Authorize(Roles = "ADMIN , USER")]
        public async Task<IActionResult> GetProduct()
        {
            var products = await productRepository.GetAll();
            return View("Product", products);
        }

    }
}

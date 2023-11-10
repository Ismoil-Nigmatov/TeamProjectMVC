using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Repository;

namespace TeamProjectMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Product()
        {
            List<Product> products = await _productRepository.GetAll(); 
            return View(products);
        }
    }
}

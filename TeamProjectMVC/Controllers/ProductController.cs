using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Dto;
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

        public async Task<ViewResult> Product(string role)
        {
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Role = role,
                Products = await _productRepository.GetAll()
            };

            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> UpdateAsync(string role, string id, ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Update(id, productDto);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> DeleteAsync(string role, string id)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Delete(id);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> CreateAsync(string role, ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Add(productDto);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }
    }
}


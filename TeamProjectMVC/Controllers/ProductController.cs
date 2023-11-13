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

        public async Task<ViewResult> Product(string role, string id)
        {
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = id,
                Role = role,
               
                Products = await _productRepository.GetAll()
            };

            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> UpdateAsync(string userId, string role, string id, string userName, ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Update(userId, id, userName, productDto);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = userId,
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }


        public async Task<IActionResult> DeleteAsync(string userId, string userName, string role, string id)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Delete(userId,  id, userName);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = userId,
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> CreateAsync(string userId, string userName, string role, ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Add(userId, userName,  productDto);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = userId,
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }
    }
}

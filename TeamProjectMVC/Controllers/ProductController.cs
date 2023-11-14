using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Dto;
using TeamProjectMVC.Repository;

namespace TeamProjectMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
     
        public ProductController(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<ViewResult> Product(string role, string id, string userName)
        {
            var roleProductDto = await _productRepository.RetrieveDto(id, userName, role);

            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> UpdateAsync(string userId, string role, string userName, string id,ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Update(userId, userName, id, productDto);
            var roleProductDto = await _productRepository.RetrieveDto(userId, userName, role);
            return View("Product", roleProductDto);
        }


        public async Task<IActionResult> DeleteAsync(string userId, string userName, string role, string id)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Delete(userId, userName,  id);
            var roleProductDto = await _productRepository.RetrieveDto(userId, userName, role);
            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> CreateAsync(string userId, string userName,  string role, ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Add(userId, userName,  productDto);
            var roleProductDto = await _productRepository.RetrieveDto(userId, userName, role);
            return View("Product", roleProductDto);
        }
    }
}
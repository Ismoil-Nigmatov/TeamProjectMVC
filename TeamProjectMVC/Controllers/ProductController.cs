using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Dto;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Repository;

namespace TeamProjectMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
     
        public ProductController(IProductRepository productRepository, UserManager<User> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = "ADMIN,USER")]
        public async Task<ViewResult> Product()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId!);
            var rolesAsync = await _userManager.GetRolesAsync(user);
            string role = rolesAsync[0];
            string id = userId!;
            string userName = user.UserName!;

            var roleProductDto = await _productRepository.RetrieveDto(id, userName, role);

            return View("Product", roleProductDto);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(string userId, string role, string userName, string id,ProductDTO productDto)
        {
         //   if (!ModelState.IsValid) return View("Product");
            await _productRepository.Update(userId, userName, id, productDto);
            var roleProductDto = await _productRepository.RetrieveDto(userId, userName, role);
            return View("Product", roleProductDto);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string userId, string userName, string role, string id)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Delete(userId, userName,  id);
            var roleProductDto = await _productRepository.RetrieveDto(userId, userName, role);
            return View("Product", roleProductDto);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string userId, string userName,  string role, ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Add(userId, userName,  productDto);
            var roleProductDto = await _productRepository.RetrieveDto(userId, userName, role);
            return View("Product", roleProductDto);
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Dto;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Repository;
using TeamProjectMVC.Repository.Impl;

namespace TeamProjectMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
     
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
       
        }

        public async Task<ViewResult> Product(string role, string id, string userName)
        {
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = id,
                Name = userName,
                Role = role,
                Products = await _productRepository.GetAll()
            };

            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> UpdateAsync(string userId, string role, string userName, string id,ProductDTO productDto)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Update(userId, userName, id, productDto);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = userId,
                Name = userName,
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }


        public async Task<IActionResult> DeleteAsync(string userId, string userName, string role, string id)
        {
            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Delete(userId, userName,  id);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = userId,
                Name = userName,
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }

        public async Task<IActionResult> CreateAsync(string userId, string userName,  string role, ProductDTO productDto)
        {
           
            

            if (!ModelState.IsValid) return View("Product");
            await _productRepository.Add(userId, userName,  productDto);
            RoleProductDTO roleProductDto = new RoleProductDTO
            {
                Id = userId,
                Name = userName,
                Role = role,
                Products = await _productRepository.GetAll()
            };
            return View("Product", roleProductDto);
        }
    }
}


//var user = await _userManager.GetUserAsync(HttpContext.User);
//await _productRepository.CreateProductAsync(product);
//await _productRepository.CreateAudit(product, null, "Create", user);
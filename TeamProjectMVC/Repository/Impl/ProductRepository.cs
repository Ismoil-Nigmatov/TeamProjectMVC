﻿using Microsoft.EntityFrameworkCore;
using TeamProjectMVC.Data;
using TeamProjectMVC.Dto;
using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Repository.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Get(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product ?? throw new BadHttpRequestException("Product not found.");
        }

        public async Task Add(string userId ,ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                ToTalPrice = await CalculateTotalPrice(productDto.Quantity, productDto.Price)
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync(userId);
        }


        public async Task Update(string userId, string id,  ProductDTO productDto)
        {
            var vat = _configuration["Vat"]!;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is not null)
            {
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Quantity = productDto.Quantity;
                product.ToTalPrice = (productDto.Quantity * productDto.Price) * (1 + Convert.ToDouble(vat));
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync(userId);
            }
            else
            {
                throw new BadHttpRequestException("Product not found");
            }
        }

        public async Task Delete(string userId, string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(userId);
            }
        }

        public Task<double> CalculateTotalPrice(int quantity, double price)
        {
            var vat = _configuration["Vat"]!;

            return Task.FromResult((quantity * price) * (1 + Convert.ToDouble(vat)));
        }

    }
}

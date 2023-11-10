using TeamProjectMVC.Dto;
using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> Get(string id);
        Task Add(string userId, ProductDTO productDto);
        Task Update(string userId , string id, ProductDTO productDto);
        Task Delete(string userId, string id);
    }
}

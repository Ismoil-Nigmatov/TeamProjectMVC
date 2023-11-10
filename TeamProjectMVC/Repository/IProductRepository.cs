using TeamProjectMVC.Dto;
using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> Get(string id);
        Task Add(ProductDTO productDto);
        Task Update(string id, ProductDTO productDto);
        Task Delete(string id);
    }
}

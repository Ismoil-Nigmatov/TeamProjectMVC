using TeamProjectMVC.Dto;
using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> Get(string id);
        Task Add(string userId, string userName, ProductDTO productDto);
        Task Update(string userId , string id, string userName, ProductDTO productDto);
        Task Delete(string userId, string id, string userName);
        Task<double> CalculateTotalPrice(int quantity, double price);

    }
}

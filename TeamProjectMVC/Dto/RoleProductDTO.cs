using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Dto
{
    public class RoleProductDTO
    {
        public string Role { get; set; }
        public List<Product> Products { get; set; }
    }
}

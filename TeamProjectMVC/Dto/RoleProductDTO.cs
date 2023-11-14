using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Dto
{
    public class RoleProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Product> Products { get; set; }
    }
}

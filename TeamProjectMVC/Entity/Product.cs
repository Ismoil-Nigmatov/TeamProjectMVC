namespace TeamProjectMVC.Entity
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? Name { get; set; }

        public ulong Quantity { get; set; }

        public double Price { get; set; }

        public double ToTalPrice { get; set; }
    }
}

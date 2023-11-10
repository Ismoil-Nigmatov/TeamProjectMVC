namespace TeamProjectMVC.Entity
{
    public class Product
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double ToTalPrice { get; set; }
    }
}

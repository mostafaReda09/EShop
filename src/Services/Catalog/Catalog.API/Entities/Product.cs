namespace Catalog.API.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Image { get; set; }=string.Empty;
        public double Price { get; set; }
        public string Description { get; set; }=string.Empty;
    }
}

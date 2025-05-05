using OnlineStore.Domain.Interfaces;

namespace OnlineStore.Domain.Entities
{
    public class Product : IDatabaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Review> Reviews { get; set; } = new();
    }
}

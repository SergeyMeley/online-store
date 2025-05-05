using OnlineStore.Domain.Interfaces;

namespace OnlineStore.Domain.Entities
{
    public class Category : IDatabaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}

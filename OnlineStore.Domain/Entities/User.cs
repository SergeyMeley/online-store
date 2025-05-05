using OnlineStore.Domain.Interfaces;

namespace OnlineStore.Domain.Entities
{
    public class User : IDatabaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<Review> Review { get; set; }
    }
}

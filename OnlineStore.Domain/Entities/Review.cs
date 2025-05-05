using OnlineStore.Domain.Interfaces;

namespace OnlineStore.Domain.Entities
{
    public class Review : IDatabaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; } // 1-5
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

namespace OnlineStore.Application.DTOs
{
    public class ReviewCreateDto()
    {
        public int ProductId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    };
}

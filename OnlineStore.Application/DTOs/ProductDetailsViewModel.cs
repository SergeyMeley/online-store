namespace OnlineStore.Application.DTOs
{
    public class ProductDetailsViewModel
    {
        public ProductDto Product { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }
        public ReviewCreateDto NewReview { get; set; }
    }
}

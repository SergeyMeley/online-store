using OnlineStore.Application.DTOs;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interfaces;

namespace OnlineStore.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IProductRepository _productRepo;

        public ReviewService(
            IReviewRepository reviewRepo,
            IProductRepository productRepo)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
        }

        public async Task<ReviewResult> AddReviewAsync(int productId, string userId, string text, int rating)
        {
            // Валидация
            if (rating < 1 || rating > 5)
                return ReviewResult.Failed("Рейтинг должен быть от 1 до 5");

            if (string.IsNullOrEmpty(text))
                return ReviewResult.Failed("Текст отзыва обязателен");

            // Проверка существования товара
            var productExists = await _productRepo.ExistsAsync(productId);
            if (!productExists)
                return ReviewResult.Failed("Товар не найден");

            // Создание отзыва
            var review = new Review
            {
                ProductId = productId,
                UserId = 1,
                Text = text,
                Rating = rating,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepo.AddAsync(review);
            return ReviewResult.Success(review.Id);
        }

        public Task<bool> CanUserReviewProductAsync(int productId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReviewAsync(int reviewId, string userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetAverageRatingAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId)
        {
            var reviews = await _reviewRepo.GetByProductIdAsync(productId);
            return reviews
                .Select(x => new ReviewDto(x.Id, x.Text, x.Rating, x.User?.UserName, x.CreatedAt));
        }
    }
}

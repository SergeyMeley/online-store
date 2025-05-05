using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Services
{
    public interface IReviewService
    {
        // Добавление отзыва
        Task<ReviewResult> AddReviewAsync(int productId, string userId, string text, int rating);

        // Получение отзывов для товара
        //Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId, int page = 1, int pageSize = 10);
        Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId);

        // Удаление отзыва (для админа/автора)
        Task<bool> DeleteReviewAsync(int reviewId, string userId, bool isAdmin);

        // Расчет среднего рейтинга товара
        Task<double> GetAverageRatingAsync(int productId);

        // Проверка, может ли пользователь оставить отзыв
        Task<bool> CanUserReviewProductAsync(int productId, string userId);
    }
}

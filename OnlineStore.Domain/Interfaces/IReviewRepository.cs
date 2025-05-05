// Domain/Interfaces/IReviewRepository.cs
using OnlineStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
        Task<double> GetAverageRatingAsync(int productId);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
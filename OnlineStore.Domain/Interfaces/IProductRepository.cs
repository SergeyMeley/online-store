using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Interfaces
{
    public interface IProductRepository
    {
        // Получение товара по ID
        Task<Product> GetByIdAsync(int id);

        // Получение всех товаров (с фильтрацией)
        Task<IEnumerable<Product>> GetAllAsync(
            string? searchQuery = null,
            int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null
        );

        // Проверка существования товара
        Task<bool> ExistsAsync(int id);

        // Добавление нового товара
        Task AddAsync(Product product);

        // Обновление товара
        Task UpdateAsync(Product product);

        // Удаление товара
        Task DeleteAsync(int id);

        // Получение товаров по категории (опционально)
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);

        // Получение количества товаров (для пагинации)
        Task<int> GetCountAsync(string? searchQuery = null, int? categoryId = null);
    }
}
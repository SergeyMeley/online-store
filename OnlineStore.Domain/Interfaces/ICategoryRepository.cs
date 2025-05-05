using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        // Получение категории по ID
        Task<Category> GetByIdAsync(int id);

        // Получение всех категорий
        Task<IEnumerable<Category>> GetAllAsync();

        // Проверка существования категории
        Task<bool> ExistsAsync(int id);

        // Проверка, используется ли категория товарами
        Task<bool> IsUsedByProductsAsync(int categoryId);

        // Добавление новой категории
        Task AddAsync(Category category);

        // Обновление категории
        Task UpdateAsync(Category category);

        // Удаление категории
        Task DeleteAsync(int id);
    }
}
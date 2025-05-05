using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto dto);
    }
}

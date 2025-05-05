using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interfaces;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Services;
using OnlineStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryService(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        //return new List<CategoryDto>();
        var categories = await _categoryRepo.GetAllAsync();
        return categories.Select(c => new CategoryDto(c.Id, c.Name));
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto dto)
    {
        var category = new Category { Name = dto.Name };
        await _categoryRepo.AddAsync(category);
        return new CategoryDto(category.Id, category.Name);
    }

    public async Task UpdateCategoryAsync(int id, CategoryCreateUpdateDto dto)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        if (category == null)
            throw new KeyNotFoundException("Category not found");

        category.Name = dto.Name;
        await _categoryRepo.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        if (!await _categoryRepo.ExistsAsync(id))
            throw new KeyNotFoundException("Category not found");

        // Проверка, что категория не используется товарами
        var isUsed = await _categoryRepo.IsUsedByProductsAsync(id);
        if (isUsed)
            throw new InvalidOperationException("Cannot delete category with associated products");

        await _categoryRepo.DeleteAsync(id);
    }
}
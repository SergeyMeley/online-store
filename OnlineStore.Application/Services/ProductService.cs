using OnlineStore.Application.DTOs;
using OnlineStore.Application.Services;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interfaces;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IReviewRepository _reviewRepo;

    public ProductService(
        IProductRepository productRepo,
        ICategoryRepository categoryRepo,
        IReviewRepository reviewRepo)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _reviewRepo = reviewRepo;
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        var averageRating = await _reviewRepo.GetAverageRatingAsync(id);
        var category = await _categoryRepo.GetByIdAsync(product.CategoryId);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            ToCategoryDto(product.Category)
        );
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepo.GetAllAsync();
        return products.
            Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Price, ToCategoryDto(x.Category)));
    }

    private CategoryDto ToCategoryDto(Category category)
    {
        return new CategoryDto(category.Id, category.Name);
    }

    public async Task<IEnumerable<ProductDto>> GetFilteredProductsAsync(ProductFilterDto filter)
    {
        var products = await _productRepo.GetAllAsync(filter.SearchQuery, filter.CategoryId, filter.MinPrice, filter.MaxPrice);
        return products.
            Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Price, ToCategoryDto(x.Category)));
    }

    public async Task<ProductDto> CreateProductAsync(ProductCreateUpdateDto dto)
    {
        var categoryExists = await _categoryRepo.ExistsAsync(dto.CategoryId);
        if (!categoryExists)
            throw new ArgumentException("Invalid category ID");

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId
        };

        await _productRepo.AddAsync(product);
        return await GetProductByIdAsync(product.Id); // Возвращаем созданный продукт с DTO
    }

    public async Task UpdateProductAsync(int id, ProductCreateUpdateDto dto)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        await _productRepo.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        if (!await _productRepo.ExistsAsync(id))
            throw new KeyNotFoundException("Product not found");

        await _productRepo.DeleteAsync(id);
    }
}
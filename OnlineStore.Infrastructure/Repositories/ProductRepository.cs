using OnlineStore.Domain.Interfaces;
using OnlineStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)  // Подгрузка связанной категории
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Product>> GetAllAsync(
            string? searchQuery = null,
            int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(p => p.Name.Contains(searchQuery));
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);
            return await query.ToListAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }
        public async Task<int> GetCountAsync(string? searchQuery = null, int? categoryId = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(p => p.Name.Contains(searchQuery));

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            return await query.CountAsync();
        }
    }
}
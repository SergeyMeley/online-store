using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    // DbSet для всех сущностей
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Конфигурация отношений и ограничений
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable(nameof(Products));
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(100).IsRequired(false);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable(nameof(Reviews));
            entity.HasKey(x => x.Id);
            entity.HasOne(r => r.Product)
              .WithMany(p => p.Reviews)
              .HasForeignKey(r => r.ProductId)
              .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.User)
                .WithMany(x => x.Review)
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable(nameof(Categories));
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable(nameof(Users));
            e.HasKey(x => x.Id);
        });

        // Seed данных (опционально)
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Трубопроводные детали" },
            new Category { Id = 2, Name = "Трубопроводная арматура" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product() { Id = 1, Name = "Кран шаровой", Price = 15000, CategoryId = 2 },
            new Product() { Id = 2, Name = "Отвод", Price = 20000, CategoryId = 1 }
            );

        modelBuilder.Entity<Review>().HasData(
            new Review() { Id = 1, ProductId = 1, Text = "Нормальный кран, советую", Rating = 5, UserId = 1 }
            );

        modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, UserName = "Vasya" },
            new User() { Id = 2, UserName = "Petya" }
            );
    }
}
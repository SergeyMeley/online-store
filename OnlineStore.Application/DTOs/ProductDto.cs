namespace OnlineStore.Application.DTOs
{
    public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    CategoryDto Category
);
}

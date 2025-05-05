namespace OnlineStore.Application.DTOs
{
    public record ProductCreateUpdateDto(
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    string? ImageUrl = null
);
}

namespace OnlineStore.Application.DTOs
{
    public record ProductFilterDto(
        string? SearchQuery = null,
        int? CategoryId = null,
        decimal? MinPrice = null,
        decimal? MaxPrice = null
    );
}

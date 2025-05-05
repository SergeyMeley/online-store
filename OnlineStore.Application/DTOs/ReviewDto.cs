namespace OnlineStore.Application.DTOs
{
    public record ReviewDto(
    int Id,
    string Text,
    int Rating,
    string UserName,
    DateTime CreatedAt
);
}

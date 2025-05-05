using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId);
    }
}

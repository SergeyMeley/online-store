using OnlineStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId);
    }
}

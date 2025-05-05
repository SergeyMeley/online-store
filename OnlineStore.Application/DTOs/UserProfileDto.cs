using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs
{
    public record UserProfileDto(
    string Id,
    string Email,
    string? UserName
);
}

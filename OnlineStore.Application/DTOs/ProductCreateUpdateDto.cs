using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs
{
    public record ProductFilterDto(
        string? SearchQuery = null,
        int? CategoryId = null,
        decimal? MinPrice = null,
        decimal? MaxPrice = null
    );
}

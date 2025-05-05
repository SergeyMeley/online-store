using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs
{
    public record ReviewDto(
    int Id,
    string Text,
    int Rating,
    string UserName,  // Автор отзыва
    DateTime CreatedAt
);
}

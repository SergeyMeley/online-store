using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs
{
    public class ReviewResult
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public int? ReviewId { get; }

        // Конструктор
        public ReviewResult(bool isSuccess, string? error, int? reviewId)
        {
            IsSuccess = isSuccess;
            Error = error;
            ReviewId = reviewId;
        }

        // Статические методы (оставляем без изменений)
        public static ReviewResult Success(int reviewId)
            => new ReviewResult(true, null, reviewId);

        public static ReviewResult Failed(string error)
            => new ReviewResult(false, error, null);
    }
}

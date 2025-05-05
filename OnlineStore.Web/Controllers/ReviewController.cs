using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Services;
using System.Security.Claims;

namespace Ecommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        // Внедрение зависимости через конструктор
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Получаем ID текущего пользователя
            var result = await _reviewService.AddReviewAsync(dto.ProductId, userId, dto.Text, dto.Rating);

            return result.IsSuccess
                ? Ok(new { result.ReviewId })
                : BadRequest(result.Error);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviews(int productId)
        {
            var reviews = await _reviewService.GetReviewsForProductAsync(productId);
            return Ok(reviews);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Services;

namespace OnlineStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IReviewService reviewService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _reviewService = reviewService;
        }

        [HttpGet("product")]
        public async Task<IActionResult> Catalog([FromQuery] ProductFilterDto filter)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            var products = await _productService.GetFilteredProductsAsync(filter);
            return View(products);
        }
        [HttpGet("product/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            var reviews = await _reviewService.GetReviewsForProductAsync(id);

            var model = new ProductDetailsViewModel
            {
                Product = product,
                Reviews = reviews ?? new List<ReviewDto>(),
                NewReview = new ReviewCreateDto()
                {
                    ProductId = product.Id,
                    Rating = -1,
                    Text = string.Empty
                }
            };

            return View(model);
        }

        [HttpPost("product/AddReview")]
        public async Task<IActionResult> AddReview(/*[FromForm] ReviewCreateDto dto*/ ProductDetailsViewModel vm)
        {
            var id = vm.NewReview.ProductId;
            var Rating = vm.NewReview.Rating;
            var Text = vm.NewReview.Text;

            string userId = "Petya";
            var result = await _reviewService.AddReviewAsync(id, userId, Text, Rating);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Error);
                return RedirectToAction("Details", new { id });
            }

            return RedirectToAction("Details", new { id });
        }
    }
}
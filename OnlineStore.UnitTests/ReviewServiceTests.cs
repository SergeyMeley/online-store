using Moq;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Services;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interfaces;
using static System.Net.Mime.MediaTypeNames;

[TestFixture]
public class ReviewServiceTests
{
    private Mock<IReviewRepository> _mockRepo;
    private Mock<IProductRepository> _mockRepoProduct;
    private ReviewService _service;

    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IReviewRepository>();
        _mockRepoProduct = new Mock<IProductRepository>();
        _service = new ReviewService(_mockRepo.Object, _mockRepoProduct.Object);
    }

    [Test]
    public async Task AddReview_ReturnsSuccess_WhenValid()
    {
        // Arrange
        var reviewDto = new ReviewCreateDto { ProductId = 1, Text = "Good", Rating = 5 };
        _mockRepo.Setup(x => x.AddAsync(It.IsAny<Review>())).Returns(Task.CompletedTask);

        // Act
        Random random = new Random();
        var result = await _service.AddReviewAsync(reviewDto.ProductId, "Олег", reviewDto.Text, reviewDto.Rating);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task AddReview_ReturnsError_WhenRatingInvalid()
    {
        // Arrange
        var invalidReview = new ReviewCreateDto { ProductId = 1, Text = "Bad", Rating = 6 };

        // Act

        var result = await _service.AddReviewAsync(invalidReview.ProductId, "Олег", invalidReview.Text, invalidReview.Rating);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Contains.Substring("Рейтинг"));
    }
}
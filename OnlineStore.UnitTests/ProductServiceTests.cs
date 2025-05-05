using NUnit.Framework;
using Moq;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interfaces;
using OnlineStore.Application.Services;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _mockRepo;
    private Mock<ICategoryRepository> _mockRepoCat;
    private Mock<IReviewRepository> _mockRepoRev;
    private ProductService _service;

    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepo.Object, _mockRepoCat.Object, _mockRepoRev.Object);
    }

    [Test]
    public async Task GetProductById_ReturnsProduct_WhenExists()
    {
        // Arrange
        var testProduct = new Product { Id = 1, Name = "Test", Price = 100 };
        _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(testProduct);

        // Act
        var result = await _service.GetProductByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Test"));
    }

    [Test]
    public void GetProductById_ThrowsException_WhenNotFound()
    {
        // Arrange
        _mockRepo.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Product)null);

        // Act & Assert
        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetProductByIdAsync(999));
    }
}
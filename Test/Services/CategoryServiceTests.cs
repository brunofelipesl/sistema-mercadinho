using Moq;
using Source.Application.Services;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Xunit;

namespace Test.Services;

public class CategoryServiceTests
{
    [Fact]
    public async Task AddCategoryAsync_ShouldReturnAddedCategory()
    {
        var repository = new Mock<ICategoryRepository>();
        var service = new CategoryService(repository.Object);
        var category = new Category("", "Bebidas");

        repository.Setup(r => r.AddCategoryAsync(It.IsAny<Category>()))
            .ReturnsAsync(new Category("C12345", "Bebidas"));

        var result = await service.AddCategoryAsync(category);

        Assert.NotNull(result);
        Assert.Equal("C12345", result.code);
        repository.Verify(r => r.AddCategoryAsync(It.IsAny<Category>()), Times.Once);
    }

    [Fact]
    public void ValidateCategory_ShouldReturnErrors_WhenDescriptionIsEmpty()
    {
        var repository = new Mock<ICategoryRepository>();
        var service = new CategoryService(repository.Object);

        var result = service.ValidateCategory(new Category("C1", "   "));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("descrição"));
    }

    [Fact]
    public async Task GetByCodeAsync_ShouldThrow_WhenCategoryNotFound()
    {
        var repository = new Mock<ICategoryRepository>();
        var service = new CategoryService(repository.Object);

        repository.Setup(r => r.GetByCodeAsync("X1"))
            .ReturnsAsync(Enumerable.Empty<Category>());

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.GetByCodeAsync("X1"));

        Assert.Contains("não foi encontrada", exception.Message);
    }
}

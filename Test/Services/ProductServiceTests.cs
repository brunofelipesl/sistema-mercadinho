using Moq;
using Source.Application.Services;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Xunit;

namespace Test.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task AddProductAsync_ShouldReturnAddedProduct()
    {
        var repository = new Mock<IProductRepository>();
        var service = new ProductService(repository.Object);
        var product = CreateValidProduct();

        repository.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
            .ReturnsAsync(new Product("P12345", "Café", new List<Category> { new("C1", "Bebidas") }, new List<Supplier> { new("S1", "Mercado") }, 10m, 6m, DateTime.Now.AddDays(30), 20));

        var result = await service.AddProductAsync(product);

        Assert.NotNull(result);
        Assert.Equal("P12345", result.code);
        repository.Verify(r => r.AddProductAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public void ValidateProduct_ShouldReturnErrors_WhenRequiredDataIsMissing()
    {
        var repository = new Mock<IProductRepository>();
        var service = new ProductService(repository.Object);

        var result = service.ValidateProduct(new Product("P1", "   ", null!, null!, 0m, 0m, DateTime.Now.AddDays(-1), -1));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("descrição"));
        Assert.Contains(result.Errors, e => e.Contains("categoria"));
        Assert.Contains(result.Errors, e => e.Contains("fornecedor"));
        Assert.Contains(result.Errors, e => e.Contains("preço"));
        Assert.Contains(result.Errors, e => e.Contains("custo"));
        Assert.Contains(result.Errors, e => e.Contains("validade"));
        Assert.Contains(result.Errors, e => e.Contains("estoque"));
    }

    [Fact]
    public async Task GetByCodeAsync_ShouldThrow_WhenProductNotFound()
    {
        var repository = new Mock<IProductRepository>();
        var service = new ProductService(repository.Object);

        repository.Setup(r => r.GetByCodeAsync("X1"))
            .ReturnsAsync(Enumerable.Empty<Product>());

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.GetByCodeAsync("X1"));

        Assert.Contains("não foi encontrado", exception.Message);
    }

    private static Product CreateValidProduct()
    {
        return new Product(
            "",
            "Café",
            new List<Category> { new("C1", "Bebidas") },
            new List<Supplier> { new("S1", "Mercado") },
            10m,
            6m,
            DateTime.Now.AddDays(30),
            20);
    }
}

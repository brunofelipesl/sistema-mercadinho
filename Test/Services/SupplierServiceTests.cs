using Moq;
using Source.Application.Services;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Repositories;
using Xunit;

namespace Test.Services;

public class SupplierServiceTests
{
    [Fact]
    public async Task AddSupplierAsync_ShouldReturnAddedSupplier()
    {
        var repository = new Mock<ISupplierRepository>();
        var service = new SupplierService(repository.Object);
        var supplier = new Supplier("", "Mercado Central");

        repository.Setup(r => r.AddSupplierAsync(It.IsAny<Supplier>()))
            .ReturnsAsync(new Supplier("S12345", "Mercado Central"));

        var result = await service.AddSupplierAsync(supplier);

        Assert.NotNull(result);
        Assert.Equal("S12345", result.code);
        repository.Verify(r => r.AddSupplierAsync(It.IsAny<Supplier>()), Times.Once);
    }

    [Fact]
    public void ValidateSupplier_ShouldReturnErrors_WhenNameIsEmpty()
    {
        var repository = new Mock<ISupplierRepository>();
        var service = new SupplierService(repository.Object);

        var result = service.ValidateSupplier(new Supplier("S1", "   "));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("nome"));
    }

    [Fact]
    public async Task GetByCodeAsync_ShouldThrow_WhenSupplierNotFound()
    {
        var repository = new Mock<ISupplierRepository>();
        var service = new SupplierService(repository.Object);

        repository.Setup(r => r.GetByCodeAsync("X1"))
            .ReturnsAsync(Enumerable.Empty<Supplier>());

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.GetByCodeAsync("X1"));

        Assert.Contains("não foi encontrado", exception.Message);
    }
}

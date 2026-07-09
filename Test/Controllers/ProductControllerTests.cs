using Microsoft.AspNetCore.Mvc;
using Moq;
using Source.Api.Controllers;
using Source.Application.Models.Common;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;
using Xunit;

namespace Test.Controllers;

public class ProductControllerTests
{
    [Fact]
    public async Task GetAllProducts_ShouldReturnOkWithResponse()
    {
        var service = new Mock<IProductService>();
        service.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Product> { new("P1", "Café", 10m, 6m, DateTime.Now.AddDays(30), 20) { categories = new List<Category> { new("C1", "Bebidas") }, suppliers = new List<Supplier> { new("S1", "Mercado") } } });
        var controller = new ProductController(service.Object);

        var result = await controller.GetAllProducts();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<Response<IEnumerable<Product>>>(okResult.Value);
        Assert.True(response.Success);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task CreateProduct_ShouldReturnBadRequest_WhenValidationFails()
    {
        var service = new Mock<IProductService>();
        var validationResult = new ValidationResult();
        validationResult.AddError("erro");
        service.Setup(s => s.ValidateProduct(It.IsAny<Product>())).Returns(validationResult);
        var controller = new ProductController(service.Object);

        var result = await controller.CreateProduct(new Product("P1", "   ", 0m, 0m, DateTime.Now.AddDays(-1), -1));

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<Response<Product>>(badRequest.Value);
        Assert.False(response.Success);
    }
}

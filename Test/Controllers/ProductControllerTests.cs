using Microsoft.AspNetCore.Mvc;
using Moq;
using Source.Api.Controllers;
using Source.Application.Models.Common;
using Source.Application.Models.DTOs;
using Source.Application.Utils.Extensions;
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
        var response = Assert.IsType<Response<IEnumerable<ProductDTO>>>(okResult.Value);
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

        var productDTO = new ProductDTO
        {
            Code = "P1",
            Description = "   ",
            SellingPrice = 0m,
            ExpirationDate = DateTime.Now.AddDays(-1),
            ReplacementCost = 0m,
            StockQuantity = -1,
            Categories = new List<CategoryDTO>(),
            Suppliers = new List<SupplierDTO>()
        };

        var result = await controller.CreateProduct(productDTO);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<Response<ProductDTO>>(badRequest.Value);
        Assert.False(response.Success);
    }
}

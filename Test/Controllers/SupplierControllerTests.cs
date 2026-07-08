using Microsoft.AspNetCore.Mvc;
using Moq;
using Source.Api.Controllers;
using Source.Application.Models.Common;
using Source.Domain.Entitites;
using Source.Domain.Interfaces.Services;
using Xunit;

namespace Test.Controllers;

public class SupplierControllerTests
{
    [Fact]
    public async Task GetAllSuppliers_ShouldReturnOkWithResponse()
    {
        var service = new Mock<ISupplierService>();
        service.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Supplier> { new("S1", "Mercado") });
        var controller = new SupplierController(service.Object);

        var result = await controller.GetAllSuppliers();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<Response<IEnumerable<Supplier>>>(okResult.Value);
        Assert.True(response.Success);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task CreateSupplier_ShouldReturnBadRequest_WhenValidationFails()
    {
        var service = new Mock<ISupplierService>();
        var validationResult = new ValidationResult();
        validationResult.AddError("erro");
        service.Setup(s => s.ValidateSupplier(It.IsAny<Supplier>())).Returns(validationResult);
        var controller = new SupplierController(service.Object);

        var result = await controller.CreateSupplier(new Supplier("S1", "   "));

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<Response<Supplier>>(badRequest.Value);
        Assert.False(response.Success);
    }
}

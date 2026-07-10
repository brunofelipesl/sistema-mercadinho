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

public class CategoryControllerTests
{
    [Fact]
    public async Task GetAllCategories_ShouldReturnOkWithResponse()
    {
        var service = new Mock<ICategoryService>();
        service.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Category> { new("C1", "Bebidas") });
        var controller = new CategoryController(service.Object);

        var result = await controller.GetAllCategories();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<Response<IEnumerable<CategoryDTO>>>(okResult.Value);
        Assert.True(response.Success);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task CreateCategory_ShouldReturnBadRequest_WhenValidationFails()
    {
        var service = new Mock<ICategoryService>();
        var validationResult = new ValidationResult();
        validationResult.AddError("erro");
        service.Setup(s => s.ValidateCategory(It.IsAny<Category>())).Returns(validationResult);
        var controller = new CategoryController(service.Object);

        var result = await controller.CreateCategory(new CategoryDTO { Code = "C1", Description = "   " });

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<Response<CategoryDTO>>(badRequest.Value);
        Assert.False(response.Success);
    }
}

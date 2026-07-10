using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Source.Api.Controllers;
using Source.Application.Models.Requests;
using Source.Infrastructure.Persistence.Context;
using Xunit;

namespace Test.Controllers;

public class AuthControllerTests
{
    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<AuthDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new AuthDbContext(options);
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Key"]).Returns("Xp3ScxRS/2H9C0ZX3lQMP1M182il7G3zPw/mD6dfGXw=");
        configMock.Setup(c => c["Jwt:Issuer"]).Returns("mercadinho_api");
        configMock.Setup(c => c["Jwt:Audience"]).Returns("mercadinho_client");

        var controller = new AuthController(context, configMock.Object);

        var result = await controller.Login(new LoginRequest { Username = "unknown", Password = "any" });

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal(401, unauthorized.StatusCode);
    }
}


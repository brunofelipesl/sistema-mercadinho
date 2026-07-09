using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var controller = new AuthController(context);

        var result = await controller.Login(new LoginRequest { Username = "unknown", Password = "any" });

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal(401, unauthorized.StatusCode);
    }
}


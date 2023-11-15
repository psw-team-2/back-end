using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;


namespace Explorer.Blog.Tests.Integration;

[Collection("Sequential")]
public class BlogCommentQueryTests : BaseBlogIntegrationTest
{
    public BlogCommentQueryTests(BlogTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<BlogCommentDto>;

        // Assert
        result.ShouldNotBeNull();
        
    }

    private static BlogCommentController CreateController(IServiceScope scope)
    {
        return new BlogCommentController(scope.ServiceProvider.GetRequiredService<IBlogCommentService>(), scope.ServiceProvider.GetRequiredService<IUserBlogService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}

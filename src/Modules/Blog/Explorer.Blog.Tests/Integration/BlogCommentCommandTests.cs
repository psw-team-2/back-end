using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace Explorer.Blog.Tests.Integration;

[Collection("Sequential")]
public class BlogCommentCommandTests : BaseBlogIntegrationTest
{
    public BlogCommentCommandTests(BlogTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        var newEntity = new BlogCommentDto
        {
            Text = "Good",
            CreationTime = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:00:00"), DateTimeKind.Utc),
            LastModification = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:10:00"), DateTimeKind.Utc),

        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogCommentDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Text.ShouldBe(newEntity.Text);
        result.CreationTime.ShouldBe(newEntity.CreationTime);
        result.LastModification.ShouldBe(newEntity.LastModification);
    
        // Assert - Database
        var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Text == newEntity.Text);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
        storedEntity.CreationTime.ShouldBe(newEntity.CreationTime);
        storedEntity.LastModification.ShouldBe(newEntity.LastModification);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new BlogCommentDto
        {
           
            BlogId = 1
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        var updatedEntity = new BlogCommentDto
        {
            Id = -1,
            Text = "Very good",
            UserId = -1,
            BlogId = 1,
            CreationTime = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:00:00"), DateTimeKind.Utc),
            LastModification = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:10:00"), DateTimeKind.Utc),


        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BlogCommentDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Text.ShouldBe(updatedEntity.Text);
        result.UserId.ShouldBe(updatedEntity.UserId);
        result.BlogId.ShouldBe(updatedEntity.BlogId);
        result.CreationTime.ShouldBe(updatedEntity.CreationTime);
        result.LastModification.ShouldBe(updatedEntity.LastModification);


        // Assert - Database
        var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Text == "Very good");
        storedEntity.ShouldNotBeNull();
        storedEntity.UserId.ShouldBe(updatedEntity.UserId);
        storedEntity.BlogId.ShouldBe(updatedEntity.BlogId);
        storedEntity.CreationTime.ShouldBe(updatedEntity.CreationTime);
        storedEntity.LastModification.ShouldBe(updatedEntity.LastModification);
        var oldEntity = dbContext.Comments.FirstOrDefault(i => i.Text == "Great work!");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new BlogCommentDto
        {
            Id = -1000,
            Text = "Test"
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Comments.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static BlogCommentController CreateController(IServiceScope scope)
    {

        return new BlogCommentController(scope.ServiceProvider.GetRequiredService<IBlogCommentService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}

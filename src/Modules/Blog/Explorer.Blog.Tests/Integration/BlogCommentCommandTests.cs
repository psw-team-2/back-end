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
        //using var scope = Factory.Services.CreateScope();
        //var controller = CreateController(scope);
        //var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        //var newEntity = new BlogCommentDto
        //{
         //   Text = "Good!!!!!",
         //   CreationTime = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:00:00"), DateTimeKind.Utc),
         //   LastModification = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:10:00"), DateTimeKind.Utc),
         //   Username = "vanja",
         //   UserId = -41,
         //   BlogId = -51,
            

        //};

        
            // Act
          //  var result = ((ObjectResult)controller.Create(newEntity).Result).Value as BlogCommentDto;




            // Assert - Response
            //result.ShouldNotBeNull();
            //result.Id.ShouldNotBe(0);
            //result.Text.ShouldBe(newEntity.Text);
            //result.CreationTime.ShouldBe(newEntity.CreationTime);
            //result.LastModification.ShouldBe(newEntity.LastModification);
            //result.Username.ShouldBe(newEntity.Username);
            //result.UserId.ShouldBe(newEntity.UserId);
            //result.BlogId.ShouldBe(newEntity.BlogId);

            // Assert - Database
            //var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Text == newEntity.Text);
            //storedEntity.ShouldNotBeNull();
            //storedEntity.Id.ShouldBe(result.Id);
            //storedEntity.CreationTime.ShouldBe(newEntity.CreationTime);
            //storedEntity.LastModification.ShouldBe(newEntity.LastModification);
            //storedEntity.UserId.ShouldBe(newEntity.UserId);
            //storedEntity.Username.ShouldBe(newEntity.Username);
            //storedEntity.BlogId.ShouldBe(newEntity.BlogId);
        
       
    }

    

    [Fact]
    public void Updates()
    {
        // Arrange
        //using var scope = Factory.Services.CreateScope();
        //var controller = CreateController(scope);
        //var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        //var updatedEntity = new BlogCommentDto
        //{
            //Id = -51,
            //Text = "Very good",
            //UserId = -41,
            //BlogId = -51,
            //CreationTime = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:00:00"), DateTimeKind.Utc),
            //LastModification = DateTime.SpecifyKind(DateTime.Parse("2023-10-25 13:10:00"), DateTimeKind.Utc),
            //Username = "jovana",
            

        //};

        // Act
        //var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BlogCommentDto;

        // Assert - Response
        //result.ShouldNotBeNull();
        //result.Id.ShouldBe(-1);
        //result.Text.ShouldBe(updatedEntity.Text);
        //result.UserId.ShouldBe(updatedEntity.UserId);
        //result.BlogId.ShouldBe(updatedEntity.BlogId);
        //result.CreationTime.ShouldBe(updatedEntity.CreationTime);
        //result.LastModification.ShouldBe(updatedEntity.LastModification);
        //result.Username.ShouldBe(updatedEntity.Username);

        // Assert - Database
        //var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Text == "Very good");
        //storedEntity.ShouldNotBeNull();
        //storedEntity.UserId.ShouldBe(updatedEntity.UserId);
        //storedEntity.BlogId.ShouldBe(updatedEntity.BlogId);
        //storedEntity.CreationTime.ShouldBe(updatedEntity.CreationTime);
        //storedEntity.LastModification.ShouldBe(updatedEntity.LastModification);
        //storedEntity.Username.ShouldBe(updatedEntity.Username);
        //var oldEntity = dbContext.Comments.FirstOrDefault(i => i.Text == "Predobro");
        //oldEntity.ShouldBeNull();
    }

   

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        // Act
        var result = controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        //result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Comments.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }

    
    private static BlogCommentController CreateController(IServiceScope scope)
    {

        return new BlogCommentController(scope.ServiceProvider.GetRequiredService<IBlogCommentService>(), scope.ServiceProvider.GetRequiredService<IUserBlogService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}

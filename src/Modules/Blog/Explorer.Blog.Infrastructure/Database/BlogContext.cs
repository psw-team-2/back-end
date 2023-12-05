using Explorer.Blog.Core.Domain.Blog;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<UserBlog> Blogs { get; set; }
    public DbSet<BlogComment> Comments { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");

        ConfigureBlog(modelBuilder);
        ConfigureComments(modelBuilder);

        modelBuilder.Entity<UserBlog>().Property(item => item.Ratings).HasColumnType("jsonb");

        modelBuilder.Entity<UserBlog>()
            .Property(item => item.TourReport).HasColumnType("jsonb");
    }
    private static void ConfigureBlog(ModelBuilder modelBuilder)
    {
    }

    private static void ConfigureComments(ModelBuilder modelBuilder)
    {
     

    

    }
}
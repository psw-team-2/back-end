using Explorer.Blog.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;

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
    }
    private static void ConfigureBlog(ModelBuilder modelBuilder)
    {
       
    }

    private static void ConfigureComments(ModelBuilder modelBuilder)
    {
     

    

    }
}
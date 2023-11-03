using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }

    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubRequest> ClubRequests { get; set; }

    public DbSet<TourPreference> TourPreferences { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    public DbSet<ApplicationReview> ApplicationReview { get; set; }

    public DbSet<Follow> Follows { get; set; }

    public DbSet<Message> Messages { get; set; }


    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

      
    }
}
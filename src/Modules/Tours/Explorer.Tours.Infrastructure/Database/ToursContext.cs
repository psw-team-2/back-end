using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }

    public DbSet<Tour> Tour { get; set; }
    public DbSet<CheckPoint> CheckPoint { get; set; }
    public DbSet<TouristSelectedEquipment> TouristSelectedEquipment { get; set; }
    public DbSet<Core.Domain.Object> Object { get; set; }
    public DbSet<TourReview> TourReview { get; set; }
    public DbSet<TourProblem> TourProblems { get; set; }
    public DbSet<TouristPosition> TouristPosition { get; set; }
    public DbSet<TourExecution> TourExecutions { get; set; }
    public DbSet<Secret> Secrets { get; set; }

    public DbSet<TourPurchaseToken> TourPurchaseToken { get; set; }
    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<TourProblem>()
            .HasOne<Tour>()
            .WithOne()
            .HasForeignKey<TourProblem>(s => s.TourId);

    }
}
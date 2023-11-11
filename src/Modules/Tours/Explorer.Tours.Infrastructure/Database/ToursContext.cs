using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
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
    public DbSet<TourProblemResponse> TourProblemResponse { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<TourProblem>()
            .HasOne<Tour>()
            .WithOne()
            .HasForeignKey<TourProblem>(s => s.TourId);


    }

    public async Task<List<TourProblem>> GetTourProblemsByTourId(long tourId)
    {
        return await TourProblems
            .Where(tourProblem => tourProblem.TourId == tourId)
            .ToListAsync();
    }

    public async Task<List<TourProblem>> GetTourProblemsByTouristId(long touristId)
    {
        return await TourProblems
            .Where(tourProblem => tourProblem.TouristId == touristId)
            .ToListAsync();
    }
}
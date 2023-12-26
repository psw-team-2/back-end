using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

using Microsoft.EntityFrameworkCore.Internal;
using Explorer.Payments.Core.Domain;

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
    public DbSet<PublicRequest> PublicRequests { get; set; }
    public DbSet<TourProblemResponse> TourProblemResponse { get; set; }
    public DbSet<ComposedTour> ComposedTour { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseToken { get; set; }

    //    public DbSet<Explorer.Stakeholders.Core.Domain.User> StakeholdersUser { get; set; }

    public DbSet<CheckpointVisited> CheckpointVisited { get; set; }
    public DbSet<TourExecution> TourExecutions { get; set; }
    public DbSet<Secret> Secrets { get; set; }
    public DbSet<Bundle> Bundles { get; set; }
    public DbSet<TourSale> TourSales { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<FavouriteItem> FavouriteItems { get; set; }
    public DbSet<Giftcard> Giftcards { get; set; }
    
    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

       
        modelBuilder.Entity<TourProblem>()
            .HasOne<Tour>() // Dependent entity
            .WithMany() // Principal entity
            .HasForeignKey(tp => tp.TourId);
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


        /*modelBuilder.Entity<OrderItem>().Property(item => item.Price).HasColumnType("jsonb");
        modelBuilder.Entity<ShoppingCart>().Property(sc => sc.TotalPrice).HasColumnType("jsonb");
        modelBuilder.Entity<Tour>().Property(sc => sc.Price).HasColumnType("jsonb");*/


    }
}
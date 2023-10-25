﻿using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Core.Domain.Object> Object { get; set; }


    public DbSet<TourReview> TourReview { get; set; }


    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
    }
}
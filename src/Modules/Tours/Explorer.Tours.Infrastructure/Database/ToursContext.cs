﻿using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourProblem> TourProblems { get; set; }


    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<TourProblem>()
            .HasOne<Tour>()
            .WithOne()
            .HasForeignKey<TourProblem>(s => s.MockTourId);
    }
}
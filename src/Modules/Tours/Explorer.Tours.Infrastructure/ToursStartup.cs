using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Author;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<ITourProblemService, TourProblemService>();
        services.AddScoped<ICheckPointService, CheckPointService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IUserAccountAdministrationService, UserAccountAdministrationService>();
        services.AddScoped<ITouristSelectedEquipmentService, TouristSelectedEquipmentService>();
        services.AddScoped<IObjectService, ObjectService>();
        services.AddScoped<ITourReviewService, TourReviewService>();
        services.AddScoped<IPublicRequestService, PublicRequestService>();
        services.AddScoped<ITourProblemResponseService, TourProblemResponseService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();
        services.AddScoped<ICheckpointVisitedService, CheckpointVisitedService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
        services.AddScoped<ISecretService, SecretService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<CheckPoint>), typeof(CrudDatabaseRepository<CheckPoint, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TouristSelectedEquipment>), typeof(CrudDatabaseRepository<TouristSelectedEquipment, ToursContext>));
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<ITouristSelectedEquipmentRepository, TouristSelectedEquipmentRepository>();
        services.AddScoped(typeof(ICrudRepository<Core.Domain.Object>), typeof(CrudDatabaseRepository<Core.Domain.Object, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourReview>), typeof(CrudDatabaseRepository<TourReview, ToursContext>));
        services.AddScoped<ITourReviewRepository, TourReviewRepository>();
        services.AddScoped(typeof(ICrudRepository<TourProblem>), typeof(CrudDatabaseRepository<TourProblem, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<CheckpointVisited>), typeof(CrudDatabaseRepository<CheckpointVisited, ToursContext>));
        services.AddScoped<ITourRepository, TourRepository>();
        services.AddScoped<ITourExecutionRepository, TourExecutionRepository>();
        services.AddScoped(typeof(ICrudRepository<TourExecution>), typeof(CrudDatabaseRepository<TourExecution, ToursContext>));
        services.AddScoped<ICheckpointVisitedRepository, CheckpointVisitedRepository>();
        services.AddScoped<ISecretRepository, SecretRepository>();
        services.AddScoped(typeof(ICrudRepository<PublicRequest>), typeof(CrudDatabaseRepository<PublicRequest, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourProblemResponse>), typeof(CrudDatabaseRepository<TourProblemResponse, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, ToursContext>));
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped(typeof(ICrudRepository<OrderItem>), typeof(CrudDatabaseRepository<OrderItem, ToursContext>));
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped(typeof(ICrudRepository<TourPurchaseToken>), typeof(CrudDatabaseRepository<TourPurchaseToken, ToursContext>));



        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}
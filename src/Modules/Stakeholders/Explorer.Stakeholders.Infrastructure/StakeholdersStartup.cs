using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IApplicationReviewService, ApplicationReviewService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();

        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IClubRequestService, ClubRequestService>();
        services.AddScoped<IClubMessageService, ClubMessageService>();

        services.AddScoped<IUserAccountAdministrationService, UserAccountAdministrationService>();
        //services.AddScoped<ITourPreferenceService, TourPreferenceService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IProfileRepository, ProfileDatabaseRepository>();

        //services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<IFollowRepository, FollowDatabaseRepository>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IMessageRepository, MessageDatabaseRepository>();

        services.AddScoped<IAuthorRequestService, AuthorRequestService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ApplicationReview>), typeof(CrudDatabaseRepository<ApplicationReview, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ClubRequest>), typeof(CrudDatabaseRepository<ClubRequest, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ClubMessage>), typeof(CrudDatabaseRepository<ClubMessage, StakeholdersContext>));

        //services.AddScoped(typeof(ICrudRepository<TourPreference>), typeof(CrudDatabaseRepository<TourPreference, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Profile>), typeof(CrudDatabaseRepository<Profile, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped<IApplicationReviewRepository, ApplicationReviewDatabaseRepository>();

        //services.AddScoped(typeof(ICrudRepository<Follow>), typeof(CrudDatabaseRepository<Follow, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Message>), typeof(CrudDatabaseRepository<Message, StakeholdersContext>));
        services.AddScoped<IMessageRepository, MessageDatabaseRepository>();

        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped<IProfileRepository, ProfileDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<AuthorRequest>), typeof(CrudDatabaseRepository<AuthorRequest, StakeholdersContext>));


        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}
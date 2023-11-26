using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Infrastructure.Database;

namespace Explorer.Payments.Infrastructure
{
    public static class PaymentsStartup
    {
        public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IPurchaseReportService, PurchaseReportService>();
            services.AddScoped<IPaymentNotificationService, PaymentNotificationService>();
            services.AddScoped<IWalletService, WalletService>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped(typeof(ICrudRepository<OrderItem>), typeof(CrudDatabaseRepository<OrderItem, PaymentsContext>));
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped(typeof(ICrudRepository<PurchaseReport>), typeof(CrudDatabaseRepository<PurchaseReport, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<PaymentNotification>), typeof(CrudDatabaseRepository<PaymentNotification, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<Wallet>), typeof(CrudDatabaseRepository<Wallet, PaymentsContext>));
            services.AddScoped<IWalletRepository, WalletRepository>();

            services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
        }
    }
}

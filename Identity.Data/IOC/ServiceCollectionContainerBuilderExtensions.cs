using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.Data.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        public static IServiceCollection AddMsSqlServerDbContext<TContext>(this IServiceCollection services, string connectionName) where TContext : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();

            if (serviceProvider is null)
                throw new NullReferenceException(nameof(serviceProvider));

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddDbContext<TContext>(options => options.UseSqlServer(configuration.GetConnectionString(connectionName)));

            return services;
        }
    }
}

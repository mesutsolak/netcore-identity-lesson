using Identity.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Identity.Domain.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            if (serviceProvider is null)
                throw new NullReferenceException(nameof(serviceProvider));

            return services;
        }
    }
}

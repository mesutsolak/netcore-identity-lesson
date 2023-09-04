using Identity.Core.Helpers.Abstract;
using Identity.Core.Helpers.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Core.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<ISecurityHelper, SecurityHelper>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<IUserHelper, UserHelper>();

            return services;
        }
    }
}

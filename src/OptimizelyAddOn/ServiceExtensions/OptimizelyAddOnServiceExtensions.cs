using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OptimizelyAddOn.ServiceExtensions;

public static class OptimizelyAddOnServiceExtensions
{
    public static IServiceCollection AddOptimizelyAddOn(
        this IServiceCollection services,
        Action<AuthorizationOptions>? authorizationOptions = null)
    {
        // Authorization
        if (authorizationOptions != null)
        {
            services.AddAuthorization(authorizationOptions);
        }
        else
        {
            var allowedRoles = new List<string> { "CmsAdmins", "Administrator", "WebAdmins" };
            services.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(OptimizelyAddOnConstants.AuthorizationPolicy, policy =>
                {
                    policy.RequireRole(allowedRoles);
                });
            });
        }

        return services;
    }

    public static IApplicationBuilder UseOptimizelyAddOn(this IApplicationBuilder builder)
    {
        // Set up your pipelines here.

        return builder;
    }
}

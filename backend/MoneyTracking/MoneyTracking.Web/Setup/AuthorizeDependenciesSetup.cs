using Microsoft.AspNetCore.Authorization;
using MoneyTracking.Web.Authorization;
using MoneyTracking.Web.Authorization.Interfaces;

namespace MoneyTracking.Web.Setup
{
    public static class AuthorizeDependenciesSetup
    {
        public static void AddAuthorizeDependencies(this IServiceCollection services)
        {
            services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
            services.AddSingleton<IAuthorizationHandler, ApiKeyHandler>();
        }
    }
}
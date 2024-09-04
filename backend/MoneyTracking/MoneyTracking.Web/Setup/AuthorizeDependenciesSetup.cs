using Microsoft.AspNetCore.Authorization;
using MoneyTracking.Web.Authorization.Interfaces;
using MoneyTracking.Web.Authorization;

namespace MoneyTracking.Web.Setup
{
    public static class AuthorizeDependenciesSetup
    {
        public static void AddAuthorizeDependencies(this IServiceCollection services)
        {
            services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, ApiKeyHandler>();
        }
    }
}
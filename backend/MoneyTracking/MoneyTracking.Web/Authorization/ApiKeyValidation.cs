using MoneyTracking.Web.Authorization.Interfaces;

namespace MoneyTracking.Web.Authorization
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidApiKey(string providedApiKey)
        {
            if (string.IsNullOrEmpty(providedApiKey))
                return false;

            string? validApiKey = _configuration.GetValue<string>(AuthConfig.AuthSection);

            return string.Equals(validApiKey, providedApiKey, StringComparison.Ordinal);
        }
    }
}
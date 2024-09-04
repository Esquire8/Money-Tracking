namespace MoneyTracking.Web.Authorization.Interfaces
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string providedApiKey);
    }
}
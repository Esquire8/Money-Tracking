namespace MoneyTracking.Web.Models.IncomeModels
{
    public record IncomeUpdate(int ToUpdateIncomeId, string Description, decimal Amount, int UpdateIncomeCategoryId)
    {
    }
}
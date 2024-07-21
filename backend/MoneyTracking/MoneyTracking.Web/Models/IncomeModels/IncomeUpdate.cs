namespace MoneyTracking.Web.Models.IncomeModels
{
    public record IncomeUpdate(int IncomeId, string? Description, decimal Amount, int IncomeCategoryId)
    {
    }
}
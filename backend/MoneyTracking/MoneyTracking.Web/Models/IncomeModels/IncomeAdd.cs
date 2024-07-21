namespace MoneyTracking.Web.Models.IncomeModels
{
    public record IncomeAdd(int UserId, int IncomeCategoryId, decimal Amount, string? Description)
    { }
}
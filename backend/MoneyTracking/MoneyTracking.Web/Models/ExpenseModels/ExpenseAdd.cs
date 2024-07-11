namespace MoneyTracking.Web.Models.ExpenseModels
{
    public record ExpenseAdd(int UserId, int ExpenseCategoryId, decimal Amount, string? Description)
    {
    }
}
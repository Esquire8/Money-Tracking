namespace MoneyTracking.Web.Models.ExpenseModels
{
    public record ExpenseUpdate(int ExpenseId, decimal Amount, string? Description, int ExpenseCategoryId)
    {
    }
}
namespace MoneyTracking.Web.Models.ExpenseCategoryModels
{
    public record ExpenseCategoryUpdate(int ExpenseCategoryId, string UpdateExpenseCategoryName, int? UpdateParentId)
    {
    }
}
namespace MoneyTracking.Web.Models.ExpenseCategoryModels
{
    public record ExpenseCategoryAdd(string CategoryName, int? ParentId)
    {
    }
}
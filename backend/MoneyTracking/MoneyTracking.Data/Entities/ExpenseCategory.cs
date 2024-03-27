using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Entities;

public class ExpenseCategory : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ParentId { get; set; }
}
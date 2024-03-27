using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Entities;

public class IncomeCategory : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
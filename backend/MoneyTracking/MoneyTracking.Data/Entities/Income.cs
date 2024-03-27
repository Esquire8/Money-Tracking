using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Entities;

public class Income : IEntity
{
    public int Id { get; set; }
    public IncomeCategory Category { get; set; } = null!;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime IncomeDate { get; set; }
    public User User { get; set; } = null!;
}
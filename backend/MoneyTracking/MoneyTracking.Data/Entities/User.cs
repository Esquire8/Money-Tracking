using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }

    public List<Income> Incomes = [];
    public List<Expense> Expenses = [];
}
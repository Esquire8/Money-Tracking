using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Entities
{
    public class Expense : IEntity
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime ExpenseDate { get; set; }

        public User User { get; set; } = null!;

        public ExpenseCategory ExpenseCategory { get; set; } = null!;
    }
}

using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Models
{
    public class ExpenseCategory : ICategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Parentid { get; set; }

        public List<Expense> Expenses { get; set; } = [];
    }
}

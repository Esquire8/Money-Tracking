using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Models
{
    public class IncomeCategory : ICategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<Income> Incomes { get; set; } = new();
    }
}

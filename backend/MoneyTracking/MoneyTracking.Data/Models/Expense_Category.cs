namespace MoneyTracking.Data.Models
{
    public class Expense_Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Parent_id { get; set; }

        public List<Expense> Expenses { get; set; } = [];
    }
}

namespace MoneyTracking.Data.Models
{
    public class Expense
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public DateTime Expense_Date { get; set; }

        public Guid User_id { get; set; }

        public User? User { get; set; }

        public Guid Expense_Category_Id { get; set; }

        public Expense_Category? Expense_Category { get; set; }
    }
}

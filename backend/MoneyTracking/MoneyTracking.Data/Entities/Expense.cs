namespace MoneyTracking.Data.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int ExpenseCategoryId { get; set; }

        public ExpenseCategory? ExpenseCategory { get; set; }
    }
}

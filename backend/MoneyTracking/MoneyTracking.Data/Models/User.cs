namespace MoneyTracking.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime Registration_Date { get; set; }

        public List<Income> Incomes { get; set; } = [];

        public List<Expense> Expenses { get; set; } = [];
    }
}

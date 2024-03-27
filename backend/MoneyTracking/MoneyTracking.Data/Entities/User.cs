using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Models
{
    public class User : IEntity
    {
        public int Id { get ; set ; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime RegistrationDate { get; set; }

        public List<Income> Incomes { get; set; } = new();

        public List<Expense> Expenses { get; set; } = new();
    }
}

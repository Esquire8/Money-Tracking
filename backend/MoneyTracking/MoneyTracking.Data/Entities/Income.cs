using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Models
{
    public class Income : IEntity
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime IncomeDate { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int IncomeCategoryId { get; set; }

        public IncomeCategory? IncomeCategory {  get; set; }
    }
}

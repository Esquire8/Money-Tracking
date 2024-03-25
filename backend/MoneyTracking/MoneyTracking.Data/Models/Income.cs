namespace MoneyTracking.Data.Models
{
    public class Income
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public DateTime Income_Date { get; set; }

        public Guid User_id { get; set; }

        public User? User { get; set; }

        public Guid Income_Category_Id { get; set; }

        public Income_Category? Income_Category {  get; set; }
    }
}

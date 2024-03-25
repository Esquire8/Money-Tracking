namespace MoneyTracking.Data.Models
{
    public class Income_Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Income> Incomes { get; set; } = [];
    }
}

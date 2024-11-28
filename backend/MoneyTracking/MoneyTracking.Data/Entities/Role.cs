using MoneyTracking.Data.Interfaces;

namespace MoneyTracking.Data.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
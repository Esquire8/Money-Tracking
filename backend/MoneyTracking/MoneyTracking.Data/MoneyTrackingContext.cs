using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;

namespace MoneyTracking.Data
{
    public class MoneyTrackingContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<IncomeCategory> IncomeCategories { get; set; }

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public MoneyTrackingContext(DbContextOptions<MoneyTrackingContext> options)
            : base(options)
        {

        }

    }
}

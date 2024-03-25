using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Models;

namespace MoneyTracking.Data
{
    public class MoneyTrackingContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Income_Category> Income_Categories { get; set; }

        public DbSet<Expense_Category> Expense_Categories { get; set; }

        public MoneyTrackingContext(DbContextOptions<MoneyTrackingContext> options)
            : base(options)
        {

        }

    }
}

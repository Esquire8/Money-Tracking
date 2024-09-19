using Microsoft.EntityFrameworkCore;
using MoneyTracking.Data.Entities;
using MoneyTracking.Data.Repositories.UserRep;

namespace MoneyTracking.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MoneyTrackingContext _context;

        public UserRepository(MoneyTrackingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Login == login);
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
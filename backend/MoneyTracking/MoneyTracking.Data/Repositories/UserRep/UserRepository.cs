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

        //Получаем всех пользователей
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        //Получаем пользователя по Id
        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        //Добавляем пользователя
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
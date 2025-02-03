using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data;
using SimpleAPI.Models;

namespace SimpleAPI.Services.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {

        private readonly SQLiteContext _context;

        public UserRepository(SQLiteContext context)
        {
            _context = context;
        }


        public async Task DeleteUserAsync(int userId)
        {
            User? user = await GetUserByIDAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            _context.Users.Remove(user);
        }

        public async Task<User?> GetUserByIDAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task InsertUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Data;
using UserManagementSystem.Models;
using UserManagementSystem.Repository.Interface;

namespace UserManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email && u.DeletedAt == null);
        }

        public async Task SoftDelete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                user.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .ToListAsync();
        }
        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}

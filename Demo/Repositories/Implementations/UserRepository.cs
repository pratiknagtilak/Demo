using Demo.Data;
using Demo.Models;
using Demo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
    .Include(u => u.Role)
        .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
    .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddRefreshTokenAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
        }
    }
}

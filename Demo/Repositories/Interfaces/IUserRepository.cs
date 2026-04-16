using Demo.Models;

namespace Demo.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();

        Task AddRefreshTokenAsync(RefreshToken token);
    }
}

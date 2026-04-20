using Demo.DTOs;

namespace Demo.Services.Interfaces
{
    public interface IAuthService
    {
        Task<object> RegisterAsync(RegisterDto dto);
        Task<object> LoginAsync(LoginDto dto);
    }
}

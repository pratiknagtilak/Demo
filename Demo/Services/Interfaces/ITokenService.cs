using Demo.Models;

namespace Demo.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, List<string> permissions);
        string GenerateRefreshToken();
    }
}

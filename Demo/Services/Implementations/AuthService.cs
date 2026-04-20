using Demo.DTOs;
using Demo.Models;
using Demo.Repositories.Interfaces;
using Demo.Services.Interfaces;

namespace Demo.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IUserRepository userRepository , ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<object> RegisterAsync(RegisterDto dto)
        {
            Console.WriteLine($"Incoming Email: {dto.Email}");
           // Console.WriteLine($"Existing User Found: {existingUser != null}");
            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
                return "User already exists";

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                RoleId = 2 // Default User Role
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

           
            return new
            {
                success = true,
                message = "User registered successfully"
            };

        }

        public async Task<object> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return "Invalid credentials";

            var permissions = user.Role.RolePermissions
                .Select(rp => rp.Permission.Name)
                .ToList();

            var accessToken = _tokenService.GenerateAccessToken(user, permissions);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _userRepository.AddRefreshTokenAsync(refreshTokenEntity);
            await _userRepository.SaveChangesAsync();

            return new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}

using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.Repositories.Repositories;
using EventFlowerExchange.services.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EventFlowerExchange.services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }

        // Đăng ký người dùng
        public async Task<UserDto> RegisterAsync(UserDto userDto)
        {
            // Kiểm tra xem email đã tồn tại chưa
            if (await _userRepository.GetUserByEmailAsync(userDto.Email) != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            // Tạo người dùng mới
            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password), // Mã hóa mật khẩu
                Role = userDto.Role
            };

            await _userRepository.AddUserAsync(user);

            return userDto; // Trả về DTO nếu cần
        }

        // Đăng nhập người dùng
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                Console.WriteLine("Login data is null.");
                throw new ArgumentNullException(nameof(loginDto), "Login data cannot be null.");
            }

            Console.WriteLine("Fetching user by email...");
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            Console.WriteLine("Verifying password...");
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                Console.WriteLine("Password verification failed.");
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            Console.WriteLine("Generating token...");
            var token = GenerateJwtToken(user);
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token generation failed.");
                throw new Exception("Token generation failed.");
            }

            Console.WriteLine("Saving token to database...");
            var tokenEntity = new Token
            {
                UserId = user.Id,
                Token1 = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsRevoked = false
            };

            await _tokenRepository.AddTokenAsync(tokenEntity);
            Console.WriteLine("Token saved successfully.");

            return token;
        }



        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Chuyển đổi User thành UserDto để trả về
            return new UserDto
            {
                FullName = user.FullName!,
                Email = user.Email!,
                Role = user.Role!,
                Password = null! // Không trả về mật khẩu
            };
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Kiểm tra mật khẩu cũ
            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("Old password is incorrect.");
            }

            // Mã hóa mật khẩu mới và cập nhật
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task InvalidateTokenAsync(string tokenValue)
        {
            var existingToken = await _tokenRepository.GetTokenByValueAsync(tokenValue);
            if (existingToken == null)
            {
                throw new ArgumentException("Token not found.");
            }

            if (existingToken.IsRevoked)
            {
                throw new InvalidOperationException("Token is already revoked.");
            }

            existingToken.IsRevoked = true;
            await _tokenRepository.UpdateTokenAsync(existingToken);
        }

        // Tạo JWT
        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "JWT Key not found in configuration.");
            var key = Encoding.ASCII.GetBytes(jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role!)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}

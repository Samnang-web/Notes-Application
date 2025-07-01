using Microsoft.IdentityModel.Tokens;
using NoteApi.DTOs;
using NoteApi.Models;
using NoteApi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NoteApi.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var byts = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(byts);
        }

        private string GenerateToken(User user)
        {
            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: Claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(60)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var exiting = await _repo.GetByEmailAsync(dto.Email);
            if (exiting != null)
                throw new Exception("Email Already Registered.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            user.Id = await _repo.CreateAsync(user);
            return GenerateToken(user);

        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid Credentials");

            var hash = HashPassword(dto.Password);
            if (user.PasswordHash != hash)
                throw new Exception("Inavalid Credentials");

            return GenerateToken(user);
        }
    }
}

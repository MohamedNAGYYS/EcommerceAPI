

using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using EcommerceAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceAPI.Service
{
    // Inject UserRepository
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        
        public UserService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }


        // Build GenerateToken
        public string GenerateToken(User user)
        {
            // Get key from config
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };


            // Create the Token
            var token = new JwtSecurityToken(
                issuer:_config["Jwt:Issuer"],
                audience : _config["Jwt:Audience"],
                claims:claims,
                expires:DateTime.UtcNow.AddHours(1),
                signingCredentials:credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        // Build Register
        public async Task<bool> RegisterService(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword){ throw new ArgumentException("Passwords do not match"); }

            var user = await _userRepo.RegisterRepository(dto);
            if (!user){ throw new InvalidOperationException ("Email already exists"); }
            return true;
        }


        // Build Login
        public async Task<ResponseUserDto> LoginService(LoginDto dto)
        {
        
            var user = await _userRepo.LoginRepository(dto);
            if (user==null){ throw new KeyNotFoundException("Email does not exist"); }

            var passwordVerfiy = new PasswordHasher<User>();

            if (passwordVerfiy.VerifyHashedPassword(user, user.PasswordHash, dto.Password) != PasswordVerificationResult.Success)
            { throw new ArgumentException("Passwords do not match"); }

            return new ResponseUserDto
            {
                Email = user.Email,
                Token = GenerateToken(user)
            };
        }
    }
}
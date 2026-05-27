// Gonna Implement what I defined inside IUserRepo...

// Register:
/*
1. Get user's dto
2. If email is valid:
    3. If two passwords match:
        4. Create user and add it to DB (Don't forget to hash password)
        5. Save, and return true
    false
false
*/

// Login:
/*
1. Check if email exists and valid:
    2. Check if password is correct:
        3. return Token
    false
false
*/



using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }



        // Register
        public async Task<bool> RegisterRepository(RegisterDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            
            if (user != null){ return false; }
            
            var newUser = new User
            {
                Email = dto.Email,
                Balance = 100.00m,
                Role = "User"
            };

            var passwordHasher = new PasswordHasher<User>();
            newUser.PasswordHash = passwordHasher.HashPassword(newUser, dto.Password);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            
            // Create cart that belongs to user
            var cart = new Cart{UserID = newUser.UserID};
    

            _context.Carts.Add(cart);
            
            await _context.SaveChangesAsync();
            return true;  
    
        }

        // Login
        public async Task<User> LoginRepository(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null){ return null; }

            return user;
        

        }
    }
}
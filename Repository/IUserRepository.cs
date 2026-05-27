// How can I let my repo to deal with database:
// 1. Add/Create which means Register and save Email, PasswrodHash, Role fields in DB
// 2. Check = Login
// 3. Response = Email, and Token


using EcommerceAPI.DTOs;
using EcommerceAPI.Model;

namespace EcommerceAPI.Repository
{
    public interface IUserRepository
    {
        Task<bool> RegisterRepository(RegisterDto dto);
        Task<User> LoginRepository(LoginDto dto);
    }
}
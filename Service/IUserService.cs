// Login, Register


using EcommerceAPI.DTOs;

namespace EcommerceAPI.Service
{
    public interface IUserService
    {
        Task<bool> RegisterService(RegisterDto dto);
        Task<ResponseUserDto> LoginService(LoginDto dto);
    }
}
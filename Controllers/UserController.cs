using EcommerceAPI.DTOs;
using EcommerceAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterController([FromBody] RegisterDto dto)
        {
            try
            {
                await _userService.RegisterService(dto);       
                return Ok(new {message = "You have been registered successfully"});   
        
            }
            catch(ArgumentException ex){ return BadRequest(ex.Message); }
            catch(InvalidOperationException  ex){ return Conflict(new {error = ex.Message}); }
        }


        [HttpPost("login")]
        public async Task<ActionResult<ResponseUserDto>> LoginController([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _userService.LoginService(dto);
                return Ok(result);
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
            catch(ArgumentException ex){ return BadRequest(ex.Message); }
        }
    }
}
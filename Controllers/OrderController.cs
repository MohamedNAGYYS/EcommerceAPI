// Create an endpoint for checkout method



using System.Security.Claims;
using EcommerceAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceAPI.DTOs;


namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }



        [Authorize]
        [HttpPost("checkout")]
        public async Task<ActionResult> CheckOutController()
        {
            try
            {
                var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _service.CheckOut(userid);
                return Ok(new {message = "Payment has been paid successfully"}); 

            }catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
            catch(Exception ex){ return BadRequest(ex.Message); }

        }

        [Authorize]
        [HttpGet("history")]
        public async Task<ActionResult<List<OrderHistoryDto>>> OrderHistoryController()
        {
            try
            {
                var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _service.OrderHistoryService(userid);
                return Ok(result);
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }
    }
}
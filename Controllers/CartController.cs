// Endpoints for:
// GetItems, Add, Update, Delete, Clear Cart


using Microsoft.AspNetCore.Mvc;
using EcommerceAPI.Service;
using EcommerceAPI.Model;
using Microsoft.AspNetCore.Authorization;
using EcommerceAPI.DTOs;
using System.Security.Claims;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController:ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [Authorize]
        [HttpGet("mycart")]
        public async Task<ActionResult<CartResponseDto>> GetItemsController()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _cartService.GetItemsService(userId);
                return Ok(result);
            }
            catch(KeyNotFoundException ex) { return NotFound(ex.Message); }
        }


        [Authorize]
        [HttpPost("items")]
        public async Task<ActionResult> AddToCartController([FromBody] AddToCartDto dto)
        {
            try
            {
                
                var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _cartService.AddToCartService(userid, dto);
                return Ok(new {message = "Added to cart successfully."});
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
            catch(Exception ex){ return BadRequest(ex.Message); }
        }

        [Authorize]
        [HttpPut("items")]
        public async Task<ActionResult> UpdateFromCartController([FromBody] UpdateFromCartDto dto)
        {
            try
            {
                var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _cartService.UpdateFromCartService(userid, dto);
                return Ok(result);
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }


        [Authorize]
        [HttpDelete("items/{ProductID}")]
        public async Task<ActionResult> DeleteFromCartController(int ProductID)
        {
            try
            {
                var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _cartService.DeleteFromCartService(userid, ProductID);
                return NoContent();
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }


        [Authorize]
        [HttpDelete("clear")]
        public async Task<ActionResult> ClearCartController()
        {
            try
            {
                var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _cartService.ClearCartService(userid);
                return NoContent();
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }

    

    }
}
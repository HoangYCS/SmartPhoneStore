using Data.IRespositories;
using Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartRespository _cartRespository) : ControllerBase
    {
        public class CartItemData
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public int UserId { get; set; }
        }
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(CartItemData cartItemData)
        {
            return Ok(await _cartRespository.AddItem(cartItemData.ProductId , cartItemData.Quantity, cartItemData.UserId));
        }

        [HttpDelete("delete-to-cart")]
        public async Task<IActionResult> DeleteItem(int productId, int userId)
        {
            return Ok(await _cartRespository.RemoveLine(productId, userId));
        }

        [HttpDelete("clear-cart")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            return Ok(await _cartRespository.Clear(userId));
        }

        [HttpGet("get-total-cart")]
        public async Task<IActionResult> ComputeTotalValue(int userId)
        {
            return Ok(await _cartRespository.ComputeTotalValue(userId));
        }

        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            return Ok(await _cartRespository.GetCartItems(userId));
        }
    }
}

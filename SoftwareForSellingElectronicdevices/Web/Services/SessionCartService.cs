using Data.ViewModel;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web.Helpers;
using Web.IServices;
using Web.Models;

namespace Web.Services
{
    public class SessionCartService : ICartService
    {
        private readonly ISession _session;
        private const string CartSessionKey = "Cart";
        private readonly IProductUserService _productUserService;

        public SessionCartService(IHttpContextAccessor httpContextAccessor, IProductUserService productUserService)
        {
            _session = httpContextAccessor.HttpContext!.Session;
            _productUserService = productUserService;
        }

        private List<CartLineVM> GetCart()
        {
            var cart = _session.GetString(CartSessionKey);
            return cart == null ? new List<CartLineVM>() : JsonConvert.DeserializeObject<List<CartLineVM>>(cart);
        }

        private Task SaveCartAsync(List<CartLineVM> cart)
        {
            return Task.Run(() =>
            {
                _session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
            });
        }

        public async Task AddItem(int productId, int quantity)
        {
            var cart = GetCart();
            var line = cart.FirstOrDefault(p => p.ProductDetailUserVM.Id == productId);

            if (line == null)
            {
                var product = await _productUserService.GetProductDetailUserAsync(productId);
                if (product != null)
                {
                    cart.Add(new CartLineVM
                    {
                        CartLineID = product.Id,
                        Quantity = quantity,
                        ProductDetailUserVM = product

                    });
                }
            }
            else
            {
                line.Quantity += quantity;
            }

            await SaveCartAsync(cart);
        }

        public async Task RemoveLine(int productId)
        {
            var cart = GetCart();
            var line = cart.FirstOrDefault(p => p.CartLineID == productId);

            if (line != null)
            {
                cart.Remove(line);
                await SaveCartAsync(cart);
            }
        }

        public Task<decimal> ComputeTotalValue()
        {
            var cart = GetCart();
            return Task.FromResult(cart.Sum(e => e.Quantity * e.ProductDetailUserVM.Price));
        }

        public async Task Clear()
        {
            await SaveCartAsync(new List<CartLineVM>());
        }

        public Task<List<CartLineVM>> GetCartItems()
        {
            return Task.FromResult(GetCart());
        }
    }
}

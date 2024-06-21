using Data.IRespositories;
using Data.ModelDbContext;
using Data.Models;
using Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Respositories
{
    public class CartRespository : ICartRespository
    {
        private readonly MyDBContext _dbContext;
        private readonly IProductUserRespository _productUserRespository;


        public CartRespository(MyDBContext dbContext, IProductUserRespository productUserRespository)
        {
            _dbContext = dbContext;
            _productUserRespository = productUserRespository;
        }

        public async Task<bool> AddItem(int productId, int quantity, int userId)
        {
            try
            {
                var cartItem = await _dbContext.CartDetails
                    .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.CartId == userId);

                if (cartItem == null)
                {
                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = userId,
                    };
                    _dbContext.CartDetails.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += quantity;
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Clear(int userId)
        {
            try
            {
                var cartItems = _dbContext.CartDetails.Where(ci => ci.CartId == userId);
                _dbContext.CartDetails.RemoveRange(cartItems);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<decimal> ComputeTotalValue(int userId)
        {
            var cartItems = await GetCartItems(userId);
            return cartItems.Sum(x => x.Quantity * x.ProductDetailUserVM.Price);
        }

        public async Task<List<CartLineVM>> GetCartItems(int userId)
        {
            var cartItems = await _dbContext.CartDetails.AsNoTracking()
                .Where(ci => ci.CartId == userId)
                .ToListAsync();

            var cartLineVMs = new List<CartLineVM>();

            foreach (var ci in cartItems)
            {
                var productDetailUserVM = await _productUserRespository.GetProductDetailUserAsync(ci.ProductId);
                cartLineVMs.Add(new CartLineVM
                {
                    CartLineID = ci.ProductId,
                    ProductDetailUserVM = productDetailUserVM,
                    Quantity = ci.Quantity
                });
            }

            return cartLineVMs;
        }

        public async Task<bool> RemoveLine(int productId, int userId)
        {
            try
            {
                var cartItem = await _dbContext.CartDetails
                    .FirstOrDefaultAsync(ci => ci.CartId == userId && ci.ProductId == productId);

                if (cartItem != null)
                {
                    _dbContext.CartDetails.Remove(cartItem);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

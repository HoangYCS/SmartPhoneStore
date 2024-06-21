using Data.Models;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRespositories
{
    public interface ICartRespository
    {
        Task<bool> AddItem(int productId, int quantity, int userId);
        Task<bool> RemoveLine(int productId, int userId);
        Task<decimal> ComputeTotalValue(int userId);
        Task<bool> Clear(int userId);
        Task<List<CartLineVM>> GetCartItems(int userId);
    }
}

using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRespositories
{
    public interface IProductUserRespository
    {
        Task<PageBasic<ProductUserViewModel>> GetProductsDisplayForUserAsync(List<int>? brands, List<int>? colors, int currentPage);
        Task<ProductDetailUserVM> GetProductDetailUserAsync(int idProduct);
    }
}

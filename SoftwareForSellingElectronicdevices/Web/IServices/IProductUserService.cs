using Data.ViewModel;

namespace Web.IServices
{
    public interface IProductUserService
    {
        Task<PageBasic<ProductUserViewModel>> GetProductsDisplayForUserAsync(List<int>? brands, List<int>? colors, int currentPage);
        Task<ProductDetailUserVM> GetProductDetailUserAsync(int idProdcut);
    }
}

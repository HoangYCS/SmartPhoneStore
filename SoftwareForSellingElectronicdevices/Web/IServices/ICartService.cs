using Data.ViewModel;

namespace Web.IServices
{
    public interface ICartService
    {
        Task AddItem(int productId, int quantity);
        Task RemoveLine(int productId);
        Task<decimal> ComputeTotalValue();
        Task Clear();
        Task<List<CartLineVM>> GetCartItems();
    }
}

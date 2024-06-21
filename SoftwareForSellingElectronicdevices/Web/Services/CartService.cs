using Data.ViewModel;
using Web.IServices;
using Web.Models;

namespace Web.Services
{
    public class CartService : ICartService
    {
        private readonly int _userId;
        private readonly HttpClient _httpClient;

        public CartService(int userId, HttpClient httpClient)
        {
            _userId = userId;
            _httpClient = httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                string errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorMessage}");
                throw new HttpRequestException($"HTTP request failed: {httpResponseMessage.ReasonPhrase}");
            }
        }

        private async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage httpResponseMessage) where TResponse : notnull
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsAsync<TResponse>();
            }
            else
            {
                string errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorMessage}");
                throw new HttpRequestException($"HTTP request failed: {httpResponseMessage.ReasonPhrase}");
            }
        }

        public async Task AddItem(int productId, int quantity)
        {
            var postData = new
            {
                productId = productId,
                quantity = quantity,
                userId = _userId
            };
            await HandleResponse(await _httpClient.PostAsJsonAsync("/api/Cart/add-to-cart", postData));
        }

        public async Task Clear()
        {
            await HandleResponse(await _httpClient.DeleteAsync($"/api/Cart/clear-cart?userId={_userId}"));
        }

        public async Task<decimal> ComputeTotalValue()
        {
            return await HandleResponse<decimal>(await _httpClient.GetAsync($"/api/Cart/get-total-cart?userId={_userId}"));
        }

        public async Task<List<CartLineVM>> GetCartItems()
        {
            return await HandleResponse<List<CartLineVM>>(await _httpClient.GetAsync($"/api/Cart/get-cart?userId={_userId}"));
        }

        public async Task RemoveLine(int productId)
        {
            await HandleResponse(await _httpClient.DeleteAsync($"/api/Cart/delete-to-cart?productId={productId}&userId={_userId}"));
        }
    }
}

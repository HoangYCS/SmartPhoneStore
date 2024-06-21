using Data.ViewModel;
using Web.IServices;

namespace Web.Services
{
    public class ProductUserService(HttpClient _httpclient) : IProductUserService
    {

        public async Task<ProductDetailUserVM> GetProductDetailUserAsync(int idProduct)
        {
            return await HandleResponse<ProductDetailUserVM>(await _httpclient.GetAsync($"/api/product/get-detail-product-user/{idProduct}"));
        }

        public async Task<PageBasic<ProductUserViewModel>> GetProductsDisplayForUserAsync(List<int>? brands, List<int>? colors, int currentPage)
        {
            var url = $"/api/product/get-list-product-user?page={currentPage}";

            if (brands != null && brands.Any())
            {
                foreach (var brand in brands)
                {
                    url += $"&brands={brand}";
                }
            }

            if (colors != null && colors.Any())
            {
                foreach (var color in colors)
                {
                    url += $"&colors={color}";
                }
            }

            var response = await _httpclient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await HandleResponse<PageBasic<ProductUserViewModel>>(response);
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
    }
}

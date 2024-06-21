using Data.DTOs;
using Data.Responses;
using Data.ViewModel;
using Web.IServices;

namespace Web.Services
{
    public class ProductsAdminService(HttpClient _httpClient) : IProductsAdminService
    {
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
        public async Task<PopularResponse<string>> CreateAsync(ProductDTO productDTO)
        {

           return await HandleResponse<PopularResponse<string>>(await _httpClient.PostAsJsonAsync("/api/product/create/product",productDTO));
        }

        public Task<PopularResponse<string>> DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<PopularResponse<string>> GetEntityAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ProductAdminViewModel>> GetEntitysAsync(ParametersProductAdminDTO? parameters)
        {
            string url = "/api/product/get-list-product";

            if (parameters != null)
            {
                if (parameters.draw != 0)
                {
                    url += $"?draw={parameters.draw}";
                }
                if (parameters.start != 0)
                {
                    url += $"&start={parameters.start}";
                }
                if (parameters.length != 0)
                {
                    url += $"&length={parameters.length}";
                }
            }
            return await HandleResponse<ApiResponse<ProductAdminViewModel>>(await _httpClient.GetAsync(url));
        }

        public async Task<PopularResponse<List<string>>> GetNamesAsync()
        {
            return await HandleResponse<PopularResponse<List<string>>>(await _httpClient.GetAsync("/api/product/get-name-product"));
        }

        public async Task<PopularResponse<string>> UpdateAsync(ProductDTO entityDTO)
        {
            return await HandleResponse<PopularResponse<string>>(await _httpClient.PutAsJsonAsync("/api/product/update/product", entityDTO));
        }

        public async Task<PopularResponse<List<string>>> GetConfigurationFrontCamerasAsync()
        {
            return await HandleResponse<PopularResponse<List<string>>>(await _httpClient.GetAsync("/api/product/get-front-camera"));
        }

        public async Task<PopularResponse<List<string>>> GetConfigurationBackCamerasAsync()
        {
            return await HandleResponse<PopularResponse<List<string>>>(await _httpClient.GetAsync("/api/product/get-back-camera"));
        }

        public async Task<PopularResponse<ProductDTO>> GetInforProductDTOAsync(int id)
        {
            return await HandleResponse<PopularResponse<ProductDTO>>(await _httpClient.GetAsync($"/api/Product/get-info-productdto/{id}"));
        }

        public async Task<bool> AddProductExcelAsync(RequestExcel requestExcel)
        {
            return await HandleResponse<bool>(await _httpClient.PostAsJsonAsync($"/api/Product/create-from-file-excel", requestExcel));
        }
    }
}

using Azure;
using Data.Responses;
using Web.IServices;

namespace Web.Services
{
    public class BaseService<TDTO, TRespose>(HttpClient _httpClient) : IBaseService<TDTO, TRespose>
    {
        private readonly string NameController = typeof(TDTO).Name.Replace("DTO", "");
        protected async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage httpResponseMessage) where TResponse : notnull
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
        public async Task<PopularResponse<TRespose>> CreateAsync(TDTO entityDTO)
        {
            return await HandleResponse<PopularResponse<TRespose>>(await _httpClient.PostAsJsonAsync($"api/{NameController}", entityDTO));
        }

        public async Task<PopularResponse<TRespose>> DeleteAsync(int Id)
        {
            return await HandleResponse<PopularResponse<TRespose>>(await _httpClient.DeleteAsync($"api/{NameController}/Delete{NameController}/{Id}"));
        }

        public async Task<PopularResponse<TDTO>> GetEntityAsync(int Id)
        {
            return await HandleResponse<PopularResponse<TDTO>>(await _httpClient.GetAsync($"api/{NameController}/{Id}"));
        }

        public async Task<PopularResponse<IList<TDTO>>> GetEntitysAsync()
        {
            return await HandleResponse<PopularResponse<IList<TDTO>>>(await _httpClient.GetAsync($"api/{NameController}"));
        }

        public async Task<PopularResponse<TRespose>> UpdateAsync(TDTO entityDTO)
        {
            return await HandleResponse<PopularResponse<TRespose>>(await _httpClient.PutAsJsonAsync($"api/{NameController}", entityDTO));
        }
    }
}

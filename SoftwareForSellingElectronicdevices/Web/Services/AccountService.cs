using Data.DTOs;
using Data.Responses;
using System.Net.Http.Json;
using Web.IServices;

namespace Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

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

        public async Task<PopularResponse<string>> LogInAsync(UserLoginDTO user)
        {
            return await HandleResponse<PopularResponse<string>>
                (await _httpClient.PostAsJsonAsync("/api/account/login", user));
        }

        public async Task<PopularResponse<string>> RegisterAsync(UserRegisterDTO userDTO)
        {
            return await HandleResponse<PopularResponse<string>>
                (await _httpClient.PostAsJsonAsync("/api/account/register", userDTO));
        }
    }
}

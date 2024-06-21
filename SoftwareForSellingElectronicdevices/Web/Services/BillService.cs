using Data.DTOs;
using Data.Helpers.Enums;
using Data.Responses;
using Web.IServices;

namespace Web.Services
{
    public class BillService : IBillService
    {
        private readonly HttpClient _httpClient;

        public BillService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public Task<PopularResponse<string>> CancelOrderAsync(int OderId)
        {
            throw new NotImplementedException();
        }

        public async Task<PopularResponse<string>> CreateOderAsync(CheckOutConfimDTO checkOutConfimDTO)
        {
            return await HandleResponse<PopularResponse<string>>(await _httpClient.PostAsJsonAsync("/api/bill/create-order", checkOutConfimDTO));
        }

        public async Task<PopularResponse<string>> UpdateStateDeliveryBillAsync(string BillCode, Status.TrangThaiGiaoHang status)
        {
            return await HandleResponse<PopularResponse<string>>(await _httpClient.PutAsJsonAsync("/api/bill/update-state-delivery", new { BillCode, status }));
        }

        public async Task<PopularResponse<string>> UpdateStatePayBillAsync(string BillCode, Status.TrangThaiThanhToan status)
        {
            return await HandleResponse<PopularResponse<string>>(await _httpClient.PutAsJsonAsync("/api/bill/update-state-pay", new { BillCode, status }));
        }
    }
}

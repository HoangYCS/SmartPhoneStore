using Data.DTOs;

namespace Web.Services
{
    public class PaymentService(HttpClient _httpClient) : BaseService<PaymentDTO, PaymentDTO>(_httpClient)
    {
    }
}

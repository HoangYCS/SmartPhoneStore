using Data.DTOs;

namespace Web.Services
{
    public class BatteryService(HttpClient _httpClient) : BaseService<BatteryDTO, BatteryDTO>(_httpClient)
    {
    }
}

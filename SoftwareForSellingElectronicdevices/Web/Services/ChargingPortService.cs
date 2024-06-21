using Data.DTOs;

namespace Web.Services
{
    public class ChargingPortService(HttpClient _httpClient) : BaseService<ChargingPortDTO, ChargingPortDTO>(_httpClient)
    {
    }
}

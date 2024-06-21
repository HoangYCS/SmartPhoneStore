using Data.DTOs;

namespace Web.Services
{
    public class ScreenService(HttpClient _httpClient) : BaseService<ScreenDTO, ScreenDTO>(_httpClient)
    {
    }
}

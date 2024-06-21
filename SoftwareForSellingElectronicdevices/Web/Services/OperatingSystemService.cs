using Data.DTOs;

namespace Web.Services
{
    public class OperatingSystemService(HttpClient _httpClient) : BaseService<OperatingSystemDTO, OperatingSystemDTO>(_httpClient)
    {
    }
}

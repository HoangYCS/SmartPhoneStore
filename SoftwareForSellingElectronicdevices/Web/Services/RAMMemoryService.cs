using Data.DTOs;

namespace Web.Services
{
    public class RamMemoryService(HttpClient _httpClient) : BaseService<RamMemoryDTO, RamMemoryDTO>(_httpClient)
    {
    }
}

using Data.DTOs;

namespace Web.Services
{
    public class MemoryStickService(HttpClient _httpClient) : BaseService<MemoryStickDTO, MemoryStickDTO>(_httpClient)
    {
    }
}

using Data.DTOs;

namespace Web.Services
{
    public class ROMMemoryService(HttpClient _httpClient) : BaseService<ROMMemoryDTO, ROMMemoryDTO>(_httpClient)
    {
    }
}

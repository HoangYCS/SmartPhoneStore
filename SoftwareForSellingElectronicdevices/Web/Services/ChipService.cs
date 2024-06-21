using Data.DTOs;

namespace Web.Services
{
    public class ChipService(HttpClient _httpClient) : BaseService<ChipDTO, ChipDTO>(_httpClient)
    {
    }
}

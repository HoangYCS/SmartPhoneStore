using Data.DTOs;

namespace Web.Services
{
    public class SIMTypeService(HttpClient _httpClient) : BaseService<SIMTypeDTO, SIMTypeDTO>(_httpClient)
    {
    }
}

using Data.DTOs;

namespace Web.Services
{
    public class ColorService(HttpClient _httpClient) : BaseService<ColorDTO, ColorDTO>(_httpClient)
    {
    }
}

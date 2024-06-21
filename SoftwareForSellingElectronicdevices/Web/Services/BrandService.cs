using Data.DTOs;

namespace Web.Services
{
    public class BrandService(HttpClient _httpClient) : BaseService<BrandDTO, BrandDTO>(_httpClient)
    {
    }
}

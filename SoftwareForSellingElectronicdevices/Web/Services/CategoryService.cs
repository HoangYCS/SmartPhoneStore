using Data.DTOs;

namespace Web.Services
{
    public class CategoryService(HttpClient _httpClient) : BaseService<CategoryDTO, CategoryDTO>(_httpClient)
    {
    }
}

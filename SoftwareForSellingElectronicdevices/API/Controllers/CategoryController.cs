using Data.DTOs;
using Data.IRespositories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryDTO, CategoryDTO>
    {
        public CategoryController(IBaseRespository<CategoryDTO, CategoryDTO> respository) : base(respository)
        {
        }
    }
}

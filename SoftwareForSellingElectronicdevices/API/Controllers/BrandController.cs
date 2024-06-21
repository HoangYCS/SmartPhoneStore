using Data.DTOs;
using Data.IRespositories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IBaseRespository<BrandDTO, BrandDTO> respository) : BaseController<Brand, BrandDTO, BrandDTO>(respository)
    {
    }
}

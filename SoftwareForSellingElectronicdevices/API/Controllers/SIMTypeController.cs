using Data.DTOs;
using Data.IRespositories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SIMTypeController(IBaseRespository<SIMTypeDTO, SIMTypeDTO> respository) : BaseController<SIMType, SIMTypeDTO, SIMTypeDTO>(respository)
    {
    }
}

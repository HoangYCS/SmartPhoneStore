using Data.DTOs;
using Data.IRespositories;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController(IBaseRespository<ColorDTO, ColorDTO> respository) : BaseController<ColorMS, ColorDTO, ColorDTO>(respository)
    {
    }
}

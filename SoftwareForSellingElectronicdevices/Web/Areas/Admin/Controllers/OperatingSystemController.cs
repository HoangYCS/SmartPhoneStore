using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;
using Web.Services;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OperatingSystemController(IBaseService<OperatingSystemDTO, OperatingSystemDTO> _operatingSystemService) : Controller
    {
        public async Task<IActionResult> GetOperatingSystems()
        {
            return Ok((await _operatingSystemService.GetEntitysAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OperatingSystemDTO operatingSystemDTO)
        {
            return Ok(await _operatingSystemService.CreateAsync(operatingSystemDTO));
        }
    }
}

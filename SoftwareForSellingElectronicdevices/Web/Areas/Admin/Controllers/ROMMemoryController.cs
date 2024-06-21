using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ROMMemoryController(IBaseService<ROMMemoryDTO, ROMMemoryDTO> baseService) : Controller
    {
        public async Task<IActionResult> Management()
        {
            return View((await baseService.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetROMMemories()
        {
            return Ok((await baseService.GetEntitysAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ROMMemoryDTO rOMMemoryDTO)
        {
            return Ok(await baseService.CreateAsync(rOMMemoryDTO));
        }
    }
}

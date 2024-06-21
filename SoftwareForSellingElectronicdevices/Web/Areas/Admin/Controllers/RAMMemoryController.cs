using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RAMMemoryController(IBaseService<RamMemoryDTO, RamMemoryDTO> memoryService) : Controller
    {
        public async Task<IActionResult> Management()
        {
            return View((await memoryService.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetRAMMemories()
        {
           return Ok((await memoryService.GetEntitysAsync()).ObjectReturn);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RamMemoryDTO ramMemoryDTO)
        {
            return Ok(await memoryService.CreateAsync(ramMemoryDTO));
        }
    }
}

using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;
using Web.Services;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemoryStickController(IBaseService<MemoryStickDTO, MemoryStickDTO> _memoryStickService) : Controller
    {
        public async Task<IActionResult> Management()
        {
            return View((await _memoryStickService.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetMemorySticks()
        {
            return Ok((await _memoryStickService.GetEntitysAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemoryStickDTO memoryStickDTO)
        {
            return Ok(await _memoryStickService.CreateAsync(memoryStickDTO));
        }
    }
}

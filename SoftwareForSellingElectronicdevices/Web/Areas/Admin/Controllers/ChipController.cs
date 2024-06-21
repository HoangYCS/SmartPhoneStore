using Data.DTOs;
using Data.IRespositories;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;
using Web.Services;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChipController(IBaseService<ChipDTO, ChipDTO> _respository) : Controller
    {
        public async Task<IActionResult> Management()
        {
            return View((await _respository.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetChips()
        {
            return Ok((await _respository.GetEntitysAsync()).ObjectReturn);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChipDTO chipDTO)
        {
            return Ok(await _respository.CreateAsync(chipDTO));
        }
    }
}

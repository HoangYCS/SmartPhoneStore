using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;
using Web.Services;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ScreenController(IBaseService<ScreenDTO,ScreenDTO> _screenService) : Controller
    {
        public async Task<IActionResult> GetScreens()
        {
            return Ok((await _screenService.GetEntitysAsync()).ObjectReturn);
        }
        public async Task<IActionResult> Management()
        {
            return View((await _screenService.GetEntitysAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScreenDTO screenDTO)
        {
            return Ok(await _screenService.CreateAsync(screenDTO));
        }
    }
}

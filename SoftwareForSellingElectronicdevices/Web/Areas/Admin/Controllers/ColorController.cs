using Data.DTOs;
using Data.Responses;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController(IBaseService<ColorDTO, ColorDTO> colorService) : Controller
    {
        public async Task<IActionResult> Management()
        {
            return View((await colorService.GetEntitysAsync()).ObjectReturn);
        }
        public async Task<IActionResult> Getcolors()
        {
            return Ok((await colorService.GetEntitysAsync()).ObjectReturn);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ColorDTO colorDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return Ok(new PopularResponse<ColorDTO>(false, string.Join(",", errors), default(ColorDTO)));
            }
            return Ok(await colorService.CreateAsync(colorDTO));
        }
    }
}

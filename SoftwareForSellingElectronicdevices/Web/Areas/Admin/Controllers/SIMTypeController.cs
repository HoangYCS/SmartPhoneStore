using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SIMTypeController(IBaseService<SIMTypeDTO, SIMTypeDTO> _service) : Controller
    {
        public async Task<IActionResult> GetSIMTypes()
        {
            return Ok((await _service.GetEntitysAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SIMTypeDTO model)
        {
            return Ok(await _service.CreateAsync(model));
        }
    }
}

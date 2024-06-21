using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Web.IServices;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChargingPortController(IBaseService<ChargingPortDTO, ChargingPortDTO> _service) : Controller
    {
        public async Task<IActionResult> GetChargingPorts()
        {
            return Ok((await _service.GetEntitysAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChargingPortDTO chargingPortDTO)
        {
            return Ok(await _service.CreateAsync(chargingPortDTO));
        }
    }
}

using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;
using Web.Services;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BatteryController(IBaseService<BatteryDTO, BatteryDTO> _batteryService) : Controller
    {
        public async Task<IActionResult> Management()
        {
            return View((await _batteryService.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetBatterys()
        {
            return Ok((await _batteryService.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> Create(string name)
        {
            var lstString = name.Split(' ');
            var batteryDTO = new BatteryDTO()
            {
                BatteryType = lstString[0],
                BatteryCapacity = Convert.ToInt32(lstString[1])
            };

            return Ok((await _batteryService.CreateAsync(batteryDTO)));
        }


    }
}

using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;
namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController :Controller
    {
        private readonly IBaseService<BrandDTO,BrandDTO> _brandService;

        public BrandController(IBaseService<BrandDTO, BrandDTO> brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Management()
        {
            return View((await _brandService.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetBrands()
        {
            return Ok((await _brandService.GetEntitysAsync()).ObjectReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandDTO brandDTO)
        {
            return Ok((await _brandService.CreateAsync(brandDTO)));
        }
    }
}

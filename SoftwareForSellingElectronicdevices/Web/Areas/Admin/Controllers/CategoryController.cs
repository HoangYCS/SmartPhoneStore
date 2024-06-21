using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;
using Web.Services;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(IBaseService<CategoryDTO, CategoryDTO> _baseService) : Controller
    {
        public async Task<IActionResult> Management()
        {
            return View((await _baseService.GetEntitysAsync()).ObjectReturn);
        }

        public async Task<IActionResult> GetCategories()
        {
            return Ok((await _baseService.GetEntitysAsync()).ObjectReturn);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            return Ok(await _baseService.CreateAsync(categoryDTO));
        }
    }
}

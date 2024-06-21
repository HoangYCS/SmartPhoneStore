using Microsoft.AspNetCore.Mvc;
using Web.IServices;

namespace Web.Controllers
{
    public class ProductController(IProductUserService _userService) : Controller
    {
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _userService.GetProductDetailUserAsync(id);
            return View(model);
        }
    }
}

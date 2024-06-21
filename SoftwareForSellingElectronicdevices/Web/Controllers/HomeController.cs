using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.IServices;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductUserService _productUserService;
        private readonly IBaseService<BrandDTO,BrandDTO> _brandService;
        private readonly IBaseService<ColorDTO,ColorDTO> _colorService;

        public HomeController(ILogger<HomeController> logger, IProductUserService productUserService, IBaseService<BrandDTO, BrandDTO> baseService, IBaseService<ColorDTO, ColorDTO> colorService)
        {
            _logger = logger;
            _productUserService = productUserService;
            _brandService = baseService;
            _colorService = colorService;
        }

        public async Task<IActionResult> Index(List<int>? brands, List<int>? colors, int currentPage = 1)
        {
            ViewBag.Brands = (await _brandService.GetEntitysAsync()).ObjectReturn;
            ViewBag.Colors = (await _colorService.GetEntitysAsync()).ObjectReturn;
            ViewBag.BrandURL = brands;
            ViewBag.ColorURL = colors;
            var model = await _productUserService.GetProductsDisplayForUserAsync(brands,colors, currentPage);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Web.IServices;

namespace Web.Components
{
    public class ProductManagement(IProductsAdminService productsAdminService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? idProduct = null)
        {
            if (idProduct == null)
            {
                ViewBag.Action = "ADD";
                return View(null);
            }
            ViewBag.Action = "EDIT";
            return View((await productsAdminService.GetInforProductDTOAsync(idProduct.Value)).ObjectReturn);
        }
    }
}

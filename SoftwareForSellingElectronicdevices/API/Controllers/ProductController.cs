using Data.DTOs;
using Data.IRespositories;
using Data.Responses;
using Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductsAdminRespository _productsAdminRespository, IProductUserRespository _productUserRespository) : ControllerBase
    {

        [HttpPost("create/product")]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            return Ok(await _productsAdminRespository.CreateAsync(productDTO));
        }

        [HttpPut("update/product")]
        public async Task<IActionResult> Update(ProductDTO productDTO)
        {
            return Ok(await _productsAdminRespository.UpdateAsync(productDTO));
        }

        [HttpGet("get-name-product")]
        public async Task<IActionResult> GetNames()
        {
            return Ok(await _productsAdminRespository.GetNamesAsync());
        }

        [HttpGet("get-front-camera")]
        public async Task<IActionResult> GetFrontCamera()
        {
            return Ok(await _productsAdminRespository.GetConfigurationFrontCamerasAsync());
        }

        [HttpGet("get-back-camera")]
        public async Task<IActionResult> GetBackCamera()
        {
            return Ok(await _productsAdminRespository.GetConfigurationBackCamerasAsync());
        }

        [HttpGet("get-list-product")]
        public async Task<ApiResponse<ProductAdminViewModel>> GetLstProduct([FromQuery]ParametersProductAdminDTO? parametersProduct)
        {
            return await _productsAdminRespository.GetEntitysAsync(parametersProduct);
        }

        [HttpGet("get-list-product-user")]
        public async Task<IActionResult> GetLstProductUser([FromQuery] List<int>? brands = null, [FromQuery] List<int>? colors = null, int page = 1)
        {
            return Ok(await _productUserRespository.GetProductsDisplayForUserAsync(brands, colors, page));
        }

        [HttpGet("get-detail-product-user/{id}")]
        public async Task<IActionResult> GetProductUser(int id)
        {
            return Ok(await _productUserRespository.GetProductDetailUserAsync(id));
        }

        [HttpGet("get-info-productdto/{id}")]
        public async Task<IActionResult> GetInfoProductDTO(int id)
        {
            return Ok(await _productsAdminRespository.GetInforProductDTOAsync(id));
        }

        [HttpPost("create-from-file-excel")]
        public async Task<IActionResult> CreateFromFileExcel(RequestExcel requestExcel)
        {
            return Ok(await _productsAdminRespository.AddProductExcelAsync(requestExcel.ProductExcels));
        }
    }
}

using Data.DTOs;
using Data.Responses;
using Data.ViewModel;

namespace Data.IRespositories
{
    public interface IProductsAdminRespository
    {
        Task<PopularResponse<string>> GetEntityAsync(int Id);
        Task<ApiResponse<ProductAdminViewModel>> GetEntitysAsync(ParametersProductAdminDTO? parametersProduct);
        Task<PopularResponse<string>> CreateAsync(ProductDTO productDTO);
        Task<PopularResponse<string>> UpdateAsync(ProductDTO entityDTO);
        Task<PopularResponse<string>> DeleteAsync(int Id);
        Task<PopularResponse<List<string>>> GetNamesAsync();
        Task<PopularResponse<List<string>>> GetConfigurationFrontCamerasAsync();
        Task<PopularResponse<List<string>>> GetConfigurationBackCamerasAsync();
        Task<PopularResponse<ProductDTO>> GetInforProductDTOAsync(int id);
        Task<bool> AddProductExcelAsync(List<ProductExcelDTO> productExcelDTOs);
    }
}

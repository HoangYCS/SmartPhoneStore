using Azure;
using Data.Responses;

namespace Web.IServices
{
    public interface IBaseService<TDTO, TRespose>
    {
        Task<PopularResponse<TDTO>> GetEntityAsync(int Id);
        Task<PopularResponse<IList<TDTO>>> GetEntitysAsync();
        Task<PopularResponse<TRespose>> CreateAsync(TDTO entityDTO);
        Task<PopularResponse<TRespose>> UpdateAsync(TDTO entityDTO);
        Task<PopularResponse<TRespose>> DeleteAsync(int Id);
    }
}

using Data.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRespositories
{
    public interface IBaseRespository<TDTO,TRespose>
    {
        Task<PopularResponse<TDTO>> GetEntityAsync(int Id);
        Task<PopularResponse<IList<TDTO>>> GetEntitysAsync();
        Task<PopularResponse<TRespose>> CreateAsync(TDTO entityDTO);
        Task<PopularResponse<TRespose>> UpdateAsync(TDTO entityDTO);
        Task<PopularResponse<TRespose>> DeleteAsync(int Id);

    }
}

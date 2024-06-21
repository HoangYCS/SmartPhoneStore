using AutoMapper;
using Data.IRespositories;
using Data.ModelDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories
{
    public class SIMTypeRespository<T, TDTO, TResponse>(MyDBContext _dbContext, IMapper _mapper) : BaseRespository<T, TDTO, TResponse>(_dbContext, _mapper) where T : class
    {
    }
}

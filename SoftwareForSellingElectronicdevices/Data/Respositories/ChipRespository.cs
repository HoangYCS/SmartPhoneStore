using AutoMapper;
using Data.ModelDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories
{
    public class ChipRespository<T, TDTO, TResponse> : BaseRespository<T, TDTO, TResponse> where T : class
    {
        public ChipRespository(MyDBContext _dbContext, IMapper _mapper) : base(_dbContext, _mapper)
        {
        }
    }
}

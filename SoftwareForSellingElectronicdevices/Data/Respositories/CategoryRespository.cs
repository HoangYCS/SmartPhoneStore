using AutoMapper;
using Data.ModelDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Respositories
{
    public class CategoryRespository<T, TDTO, TRespose>(MyDBContext _dbContext, IMapper _mapper) : BaseRespository<T, TDTO, TRespose>(_dbContext, _mapper) where T : class
    {
    }
}

using AutoMapper;
using Azure;
using Data.IRespositories;
using Data.ModelDbContext;
using Data.Responses;
using Microsoft.EntityFrameworkCore;

namespace Data.Respositories
{
    public class BaseRespository<T,TDTO, TRespose>(MyDBContext _dbContext,IMapper _mapper) : IBaseRespository<TDTO, TRespose> where T : class
    {
        public virtual async Task<PopularResponse<TRespose>> CreateAsync(TDTO entityDTO)
        {
            try
            {
                var entity = _mapper.Map<T>(entityDTO);
                await AddToDB(entity);
                return new PopularResponse<TRespose>(true, "Add success",default(TRespose), GetValueKey(entity));
            }
            catch (Exception ex)
            {
                return HandleFalse(ex);
            }

            async Task AddToDB(T entity)
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<PopularResponse<TRespose>> DeleteAsync(int Id)
        {
            try
            {
                var obj = await _dbContext.Set<T>().FindAsync(Id);
                if (obj != null)
                {
                    _dbContext.Set<T>().Remove(obj);
                    await _dbContext.SaveChangesAsync();
                    return new PopularResponse<TRespose>(false, "Delete not success", default(TRespose));
                }
                return new PopularResponse<TRespose>(true, "Delete success", default(TRespose));
            }
            catch (Exception ex)
            {
                return HandleFalse(ex);
            }
        }

        public virtual async Task<PopularResponse<TDTO>> GetEntityAsync(int Id)
        {
            try
            {
                var entity = await _dbContext.Set<T>().FindAsync(Id);
                if (entity == null)
                {
                    return new PopularResponse<TDTO>(false, "Not found", default(TDTO));
                }
                var entityDTO = _mapper.Map<TDTO>(entity);
                return new PopularResponse<TDTO>(true, "Get entity succsess", entityDTO);
            }
            catch (Exception ex)
            {
                 return new PopularResponse<TDTO>(false, ex.ToString()); ;
            }
        }

        public async virtual Task<PopularResponse<IList<TDTO>>> GetEntitysAsync()
        {
            try
            {
                var entities = await _dbContext.Set<T>().ToListAsync();
                var dtos = _mapper.Map<List<TDTO>>(entities); 
                return new PopularResponse<IList<TDTO>>(true, "Get List success", dtos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<PopularResponse<TRespose>> UpdateAsync(TDTO entityDTO)
        {
            try
            {
                var entity = _mapper.Map<T>(entityDTO);
                await Update(entity);

                return new PopularResponse<TRespose>(true, "Update success", default(TRespose));
            }
            catch (Exception ex)
            {
                return HandleFalse(ex);
            }

            async Task Update(T entity)
            {
                var existingEntity = await _dbContext.Set<T>().FindAsync(GetValueKey(entity)) ?? throw new Exception("Entity not found"); ;

                _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

                _dbContext.Entry(existingEntity).Properties
                    .Where(p => p.CurrentValue == null)
                    .ToList()
                    .ForEach(p => p.IsModified = false);

                await _dbContext.SaveChangesAsync();
            }
        }

        private static PopularResponse<TRespose> HandleFalse(Exception ex)
        {
            return new PopularResponse<TRespose>(false, ex.ToString());
        }


        private string GetKeyPropertyName()
        {
            var type = _dbContext.Model.FindEntityType(typeof(T));
            var key = type?.FindPrimaryKey() ?? null;
            var keyName = key?.Properties.Select(x => x.Name).FirstOrDefault();
            return keyName!;
        }

        private int GetValueKey(T entity)
        {
            return Convert.ToInt16((entity!.GetType().GetProperty(GetKeyPropertyName())!).GetValue(entity));
        }
    }
}

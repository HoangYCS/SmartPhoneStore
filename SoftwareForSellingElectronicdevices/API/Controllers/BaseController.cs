using Data.IRespositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T, TDTO, TRespose>(IBaseRespository<TDTO, TRespose> respository) : ControllerBase where T : class
    {
        protected readonly IBaseRespository<TDTO, TRespose> _respository = respository;

        [HttpGet]
        public virtual async Task<IActionResult> GetAllEntity()
        {
            return Ok(await _respository.GetEntitysAsync());
        }

        [HttpGet("{Id}")]
        public virtual async Task<IActionResult> GetEntityByID(int Id)
        {
            return Ok(await _respository.GetEntityAsync(Id));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TDTO entity)
        {
            return Ok(await _respository.CreateAsync(entity));
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(TDTO entity)
        {
            return Ok(await _respository.UpdateAsync(entity));
        }

        [HttpDelete("Delete[controller]/{Id}")]
        public virtual async Task<IActionResult> Delete(int Id)
        {
            return Ok(await _respository.DeleteAsync(Id));

        }
    }
}
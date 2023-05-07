using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using vktest.DbConnect;

namespace vktest.Controllers
{
    [ApiController]
    public abstract class ApiController<T> : ControllerBase where T : class
    {
        private ApplicationDbContext _context;
        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public virtual async Task<object> GetAllAsync(int page = 1)
        {
            var result = await _context.Set<T>().AsNoTracking().ToListAsync();
            if(result != null)
            {
                return new JsonResult(result);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public virtual async Task<object> GetOne(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if(result != null)
            {
                return result;
            }
            return NotFound();
        }

        [HttpPost]
        public virtual async Task<object> Post(T value)
        {
            await _context.Set<T>().AddAsync(value);

            var result = await _context.SaveChangesAsync();
            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public virtual async Task<object> Put(int id, [FromBody] T value)
        {
            var currentModel = await _context.Set<T>().FindAsync(id);
            _context.Entry<T>(currentModel).State = EntityState.Detached;
            if (currentModel != null)
            {
                currentModel = value;
                _context.Set<T>().Update(currentModel);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok();
                }
            }
            
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public virtual async Task<object> Delete(int id)
        {
            var currentModel = await _context.Set<T>().FindAsync(id);
            _context.Entry<T>(currentModel).State = EntityState.Detached;
            if (currentModel != null)
            {
                _context.Set<T>().Remove(currentModel);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using vktest.DbConnect;
using vktest.Models;

namespace vktest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController<User>
    {
        private ApplicationDbContext _context;
        private JsonSerializerSettings _serializerSettings;
        private IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5));
        public UserController(ApplicationDbContext context, IMemoryCache memoryCache) : base(context)
        {
            _context = context;
            _serializerSettings = new JsonSerializerSettings();
            _memoryCache = memoryCache;
            _serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        [HttpGet("/api/users/{page}", Name = "GetAllUsers")]
        public override async Task<object> GetAllAsync(int page = 1)
        {
            var result = await _context.Set<User>().AsNoTracking().Include(p=>p.UserGroup).Include(p=>p.UserState).Skip((page-1)*10).Take(10).ToListAsync();
            if (result != null)
            {
                var json = JsonConvert.SerializeObject(result, _serializerSettings);
                return Content(json, "application/json");
            }
            return NotFound();
        }
        [HttpGet("/api/user/{id}", Name = "GetUser")]
        public override async Task<object> GetOne(int id)
        {
            var result = await _context.Set<User>().AsNoTracking().Include(p => p.UserGroup).Include(p => p.UserState).FirstOrDefaultAsync(p=>p.Id == id);
            if (result != null)
            {
                var json = JsonConvert.SerializeObject(result, _serializerSettings);
                return Content(json, "application/json");
            }
            return NotFound();
        }
        [HttpPost("/api/regiser", Name = "Register")]
        public override async Task<object> Post([FromBody] User value)
        {
            _memoryCache.TryGetValue(value.Login, out object existedLogin);
            if (existedLogin == null)
            {
                _memoryCache.Set(value.Login, value.Created_Date, _cacheOptions);
                var currentLogin = await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(p => p.Login == value.Login);
                if (currentLogin == null)
                {
                    value.User_State_Id = ((int)UserState.UserStatesState.Active);
                    value.Created_Date = DateTime.UtcNow;
                    value.User_Group_Id = ((int)UserGroup.UserGroupState.User);
                    await Task.Delay(5000);
                    await _context.Set<User>().AddAsync(value);
                    var result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        var json = JsonConvert.SerializeObject(value, _serializerSettings);
                        return Content(json, "application/json");
                    }
                }
            }
            return BadRequest(error: "This login is already taken");
        }

        [HttpPut("{id}")]
        public override async Task<object> Put(int id, [FromBody] User value)
        {
            if(value.User_Group_Id == ((int)UserGroup.UserGroupState.Admin))
            {
                return BadRequest(error: "Wrong state");
            }
            var currentModel = await _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(p=>p.Id == id);
            if (currentModel != null)
            {
                currentModel = value;
                _context.Set<User>().Update(currentModel);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var json = JsonConvert.SerializeObject(currentModel, _serializerSettings);
                    return Content(json, "application/json");
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public override async Task<object> Delete(int id)
        {
            var currentModel = await _context.Set<User>().FindAsync(id);
            if (currentModel != null)
            {
                currentModel.User_State_Id = ((int)UserState.UserStatesState.Blocked);
                _context.Set<User>().Update(currentModel);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var json = JsonConvert.SerializeObject(currentModel, _serializerSettings);
                    return Content(json, "application/json");
                }
            }
            return BadRequest();
        }
    }
}
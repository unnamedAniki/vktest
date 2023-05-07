using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vktest.DbConnect;
using vktest.Models;

namespace vktest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatesController : ApiController<UserState>
    {
        public UserStatesController(ApplicationDbContext context) : base(context) 
        {
        }
    }
}

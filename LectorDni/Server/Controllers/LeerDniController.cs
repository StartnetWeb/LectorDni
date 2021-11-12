using LectorDni.Server.Data;
using LectorDni.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LectorDni.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeerDniController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IHostEnvironment _environment;

        public LeerDniController(ApplicationDbContext context, IHostEnvironment environment)
        {
            this.context = context;
            this._environment = environment;
        }

        [HttpPost]
        public async Task<ActionResult<DatosDni>> Post(DatosDni lecturaDni)
        {
            try
            {
                var user = (HttpContext.User.Identity as ClaimsIdentity);
                string id = this.User.FindFirst("name").Value;

                lecturaDni.UserId = id;

                context.DatosDni.Add(lecturaDni);
                context.SaveChanges();

                return lecturaDni;
            }
            catch (Exception e)
            {
                return lecturaDni;
            }
        }
    }
}

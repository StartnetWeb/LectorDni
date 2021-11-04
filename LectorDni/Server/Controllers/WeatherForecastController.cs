using LectorDni.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LectorDni.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;
        private readonly IHostEnvironment _environment;

        public WeatherForecastController(IHostEnvironment environment, ILogger<WeatherForecastController> logger)
        {
            this._environment = environment;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("api/WeatherForecast/Save")]
        public void Save([FromBody] string lecturaDni)
        {
            try
            {
                string filePath = Path.Combine(_environment.ContentRootPath, "Lecturas", "Lecturas.txt");

                StreamWriter sw = new StreamWriter(filePath);

                sw.WriteLine(lecturaDni);
                sw.Close();

            }
            catch (Exception e)
            {

            }
        }
    }
}

using DaLove_Server.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AzureBlobOptions _azureBlobOptions;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AzureBlobOptions azureBlobOptions)
        {
            _logger = logger;
            _azureBlobOptions = azureBlobOptions;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (string.IsNullOrEmpty(_azureBlobOptions.ConnectionString))
            {
                return Unauthorized("Failed");
            }

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(result);
        }
    }
}

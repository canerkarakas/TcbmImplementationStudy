using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TcmbImplementationStudy;
using TcmbImplementationStudy.EF;
using TcmbImplementationStudy.Models;

namespace TCMBImplementationStudy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            var dailyChanges = new Service2().Get("USD");
            return dailyChanges;
            //CRUD
            /*using (var context = new AppDbContext())
            {
                return context.Currencies.ToList();
            }*/
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace amc_hackathon_back.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {   
        [Route("/GetRoute")]
        [HttpGet]
        public void GetRoute(string start, string finish)
        { 
        
        
        }

        

        
    }
}

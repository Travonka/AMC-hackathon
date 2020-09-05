using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingController : ControllerBase
    {
        [HttpGet]
        public string GetRoute(float longitudeStart, float latitudeStart, float longitudeFinish, float latitudeFinish)
        {
            Hub test = new Hub(longitudeStart, latitudeStart, latitudeFinish, longitudeFinish);

            return test.BuildRoute();
        }

        public string Test()
        {
            return "HellWorld";
        }
    }
}

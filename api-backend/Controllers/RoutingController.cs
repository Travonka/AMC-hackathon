using Microsoft.AspNetCore.Mvc;
using System;
using Utils;

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingController : Controller
    {
        [Route("/App.html")]
        [HttpGet]
        public ActionResult App()
        {
            return View("Map2");
            
        }

        [Route("/GetRoute")]
        [HttpGet]
        public string GetRoute(string addressFrom, string addressTo)
        {
            try
            {
                var (latitudeStart, longitudeStart) = Hub.GetCoordinatesOfAddress(addressFrom);
                var (latitudeFinish, longitudeFinish) = Hub.GetCoordinatesOfAddress(addressTo);
                Hub test = new Hub(longitudeStart, latitudeStart, latitudeFinish, longitudeFinish);
                return test.BuildRoute();
            }
            catch (TooFarException)
            {
                return "# error too far";
            }
            catch (Exception e)
            { 
                return "# unhandled error: " + e.Message;
            }
        }

        [Route("/Test")]
        public IActionResult Test()
        {
           return Redirect("/GetRoute");
        }
    }
}

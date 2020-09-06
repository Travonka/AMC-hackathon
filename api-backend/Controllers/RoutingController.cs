using Microsoft.AspNetCore.Mvc;

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
            //return Content(System.IO.File.ReadAllText("../Map2.html"));
            //return Content("<html>quack</html>");
            return View("Map2");
            
        }

        [Route("/GetRoute")]
        [HttpGet]
        public string GetRoute(string addressFrom, string addressTo)
        {
            var (longitudeStart, latitudeStart) = Hub.GetCoordinatesOfAddress(addressFrom);
            var (longitudeFinish, latitudeFinish) = Hub.GetCoordinatesOfAddress(addressTo);
            Hub test = new Hub(longitudeStart, latitudeStart, latitudeFinish, longitudeFinish);
            return test.BuildRoute();
        }

        [Route("/Test")]
        public IActionResult Test()
        {
           return Redirect("/GetRoute");
        }
    }
}

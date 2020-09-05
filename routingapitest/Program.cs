using Itinero;
using Itinero.Osm.Vehicles;
using Itinero.IO.Osm;
using System.IO;
using System;
using Itinero.Profiles;
using System.Linq;
using Utils;

namespace routingapitest
{
    class Program
    {
        static void Main(string[] args)
        {
            Installer.InstallAndLog();

            var routerDb = RouterDb.Deserialize(new FileInfo(Const.PATH_TO_SERIALIZED).OpenRead());
            var profile = Itinero.Osm.Vehicles.Vehicle.Car.Fastest();

            var router = new Router(routerDb);
            var loc1 = router.Resolve(profile, 55.803f, 37.489f);
            var loc2 = router.Resolve(profile, 55.706f, 37.682f);
            
            var route = router.Calculate(profile, new[] { loc1, loc2 });
            Console.WriteLine(route);
            Console.WriteLine("\n\n");
            Console.WriteLine(string.Join("\n", route));
            Console.WriteLine("\n\n");
            Console.WriteLine(string.Join("\n", route.Select(c => c.Location())));
            Console.WriteLine("\n\n");
            Console.WriteLine(string.Join("\n", 
                route.Select(c => c.Location().Longitude + " " + c.Location().Latitude)));
        }
    }
}

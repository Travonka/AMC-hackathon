using Itinero;
using Itinero.Osm.Vehicles;
using Itinero.IO.Osm;
using System.IO;
using System;
using Itinero.Profiles;
using System.Linq;
using Itinero.LocalGeo;
using System.Diagnostics;

namespace routingapitest
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = Itinero.Osm.Vehicles.Vehicle.Car;

            var routerDb = new RouterDb();
            //const string path = @"../../../../MapData/moscow-overpass";
            const string path = @"../../../../MapData/central-fed-district-latest.osm.pbf";
            const string pathSer = path + ".ser";
            //using (var stream = new FileInfo(path).Open(FileMode.Create))

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var stream = new FileInfo(path).OpenRead())
            {
                // create the network for cars only.
                routerDb.LoadOsmData(stream, car);
                //routerDb = RouterDb.Deserialize(stream);

                //routerDb.Serialize(stream);
                
            }

            stopwatch.Stop();
            Console.WriteLine($"Loaded in {stopwatch.ElapsedMilliseconds} ms");

            using (var stream = new FileInfo(pathSer).Open(FileMode.Create))
            {
                routerDb.Serialize(stream);
            }

            return;
            var router = new Router(routerDb);
            var profile = new Profile("zhopa", ProfileMetric.DistanceInMeters, 
                new [] { "Car" },
                new Constraint[0], car);
            //var loc1 = router.Resolve(profile, 55.803f, 37.489f);
            //var loc2 = router.Resolve(profile, 55.706f, 37.682f);
            var loc1 = router.Resolve(profile, 14.053f,  -87.214f);
            var loc2 = router.Resolve(profile, 14.0377f, -87.217f);
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

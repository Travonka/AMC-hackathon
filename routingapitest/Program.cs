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
        static void Main(string[] _)
        {
            var routeBuilder = new SimpleRouteBuilder();

            var route = routeBuilder.BuildRoute((55.803f, 37.489f), (55.706f, 37.682f));

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

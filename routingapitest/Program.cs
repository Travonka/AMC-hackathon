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
            //var routeBuilder = new SimpleRouteBuilder();
            //
            //var route = routeBuilder.BuildRoute((55.803f, 37.489f), (55.706f, 37.682f));

            var loaded = InfectedBuildingsLoader.Load();

            Console.WriteLine(string.Join(", ", loaded.Select(c => c.Street + " " + c.Building)));
        }
    }
}

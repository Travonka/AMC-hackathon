using Itinero;
using Itinero.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils
{
    public sealed class SimpleRouteBuilder
    {
        private RouterDb routerDb;
        private Profile profile;
        private Router router;

        public SimpleRouteBuilder(Profile profile = null)
        {
            Installer.Install(logLevel: Installer.LogLevel.IMPORTANT_INFO);
            routerDb = RouterDb.Deserialize(new FileInfo(Const.PATH_TO_SERIALIZED).OpenRead());
            this.profile = profile ?? Itinero.Osm.Vehicles.Vehicle.Car.Fastest();
            router = new Router(routerDb);
        }

        public Route BuildRoute(
            (float latitude, float longtitude) pointFrom,
            (float latitude, float longtitude) pointTo)
        {
            var loc1 = router.Resolve(profile, 55.803f, 37.489f);
            var loc2 = router.Resolve(profile, 55.706f, 37.682f);

            return router.Calculate(profile, new[] { loc1, loc2 });
        }
    }
}

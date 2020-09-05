using HtmlAgilityPack;
using Itinero;
using Itinero.Profiles;
using System;
using System.IO;

namespace Utils
{
    public sealed class SimpleRouteBuilder
    {
        private readonly RouterDb routerDb;
        private readonly Profile profile;
        private readonly Router router;

        public SimpleRouteBuilder(Profile profile, Itinero.Profiles.Vehicle[] vehicles)
        {
            Installer.Install(logLevel: Installer.LogLevel.IMPORTANT_INFO, vehicles);
            routerDb = RouterDb.Deserialize(new FileInfo(Const.PATH_TO_SERIALIZED).OpenRead());
            this.profile = profile ?? Itinero.Osm.Vehicles.Vehicle.Car.Fastest();
            router = new Router(routerDb);
        }

        public Route BuildRoute(
            (float latitude, float longtitude) pointFrom,
            (float latitude, float longtitude) pointTo)
        {
            RouterPoint loc1 = null;
            RouterPoint loc2;
            try
            {
                loc1 = router.Resolve(profile, pointFrom.latitude, pointFrom.longtitude);
                loc2 = router.Resolve(profile, pointTo.latitude, pointTo.longtitude);

            }
            catch (Exception E)
            {
                return router.Calculate(profile, new[] { loc1, loc1 });
            }
            

            return router.Calculate(profile, new[] { loc1, loc2 });
        }
    }
}

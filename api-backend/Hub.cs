﻿using GetData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;
using Itinero;
using Itinero.Profiles;
using System.Text.Json;

namespace api_backend
{
    public class Hub
    {
        Parking Start, Finish;
        const float kmInDegree = 64;
        NearestPointerContainer<Parking> NearestPointer;
        SimpleRouteBuilder RouteBuilder;
        public Hub(float longitudeStart, float latitudeStart, float latitudeFinish, float longitudeFinish)
        {

            Start = new Parking() { Longitude = longitudeStart, Latitude = latitudeStart };
            Finish = new Parking() { Latitude = latitudeFinish, Longitude = longitudeFinish };
            NearestPointer = new NearestPointerContainer<Parking>(new Transport().Transports, 1 / 64f, 1 / 111f);
            RouteBuilder = new SimpleRouteBuilder(Itinero.Osm.Vehicles.Vehicle.Pedestrian.Fastest(), new Vehicle[] { Itinero.Osm.Vehicles.Vehicle.Pedestrian, Itinero.Osm.Vehicles.Vehicle.Bicycle } );

        }
        public string BuildRoute()
        {
            var ClosestToStart = NearestPointer.GetNearest(Start, (t) => 
            {
               var route= RouteBuilder.BuildRoute((Start.Latitude, Start.Longitude), (t.Latitude, t.Longitude));
               return route.TotalTime;
            }
            );
            var ClosestToFinish = NearestPointer.GetNearest(Finish, (t) =>
            {
                var route = RouteBuilder.BuildRoute((Finish.Latitude, Finish.Longitude), (t.Latitude, t.Longitude));
                return route.TotalTime;
            }
            );
            var Route1 = RouteBuilder.BuildRoute((Start.Latitude, Start.Longitude), (ClosestToStart.Latitude, ClosestToStart.Longitude));
            var Route2 = RouteBuilder.BuildRoute((ClosestToStart.Latitude, ClosestToStart.Longitude), (ClosestToFinish.Latitude, ClosestToFinish.Longitude));
            var Route3 = RouteBuilder.BuildRoute((ClosestToFinish.Latitude, ClosestToFinish.Longitude), (Finish.Latitude, Finish.Longitude));

            var arr = new {
                ToBicycleParking = Route1.Select(c => new[] { c.Location().Longitude, c.Location().Latitude }).ToArray(),
                FromParkingToParking = Route2.Select(c => new[] { c.Location().Longitude, c.Location().Latitude }).ToArray(),
                FromParkingToFinish = Route3.Select(c => new[] { c.Location().Longitude, c.Location().Latitude }).ToArray(),
            };

            var json = JsonSerializer.Serialize(arr);
            return json;
        }



    }
}

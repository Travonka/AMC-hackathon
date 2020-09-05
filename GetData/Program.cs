using GetData;
using Itinero.LocalGeo;
using System;
using System.Net;
using Utils;

namespace yandextransportapi
{
    class Program
    {
        public static void Main()
        {
            //var coo = new CoordinatesFinder();
            //coo.GetCoordinates("Moscow Myasnickaya st. d.10");
            Installer.Install(Installer.LogLevel.EVERYTHING, new[] { Itinero.Osm.Vehicles.Vehicle.Car, Itinero.Osm.Vehicles.Vehicle.Pedestrian });
        }
    }
}

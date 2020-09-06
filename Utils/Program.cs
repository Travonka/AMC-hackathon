using System;

namespace Utils
{   
    class Program
    {
        static void Main(string[] _)
        {
            Installer.Install(Installer.LogLevel.EVERYTHING, new[] { 
                Itinero.Osm.Vehicles.Vehicle.Car,
                Itinero.Osm.Vehicles.Vehicle.Pedestrian
                }, pathPrefix: "../../../");
        }
    }
}

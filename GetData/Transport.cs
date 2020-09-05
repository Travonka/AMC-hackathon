using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Globalization;
namespace GetData
{
   public class Transport 
    {
        const string Path = @"../GetData/TempData/BikesAdresses.txt";
        public List<Parking> Transports { get;  }
        CoordinatesFinder coordinatesFinder;
        public Transport()
        {
            coordinatesFinder = new CoordinatesFinder();
            Transports = new List<Parking>();
            GetCoordinates();
        }
        private void GetCoordinates()
        {
            using (StreamReader TextFileStream = new StreamReader(Path))
            {
                string line;
                while ((line = TextFileStream.ReadLine()) != null)
                {
                     var (latitude, longitude) = coordinatesFinder.GetCoordinates(line);


                    if (longitude != 0 && latitude != 0)
                    {
                        Transports.Add(new Parking()
                        {
                            Longitude = longitude,
                            Latitude = latitude,


                        });

                    }                   
                    
                    
                }
            }
        }

        public IEnumerator<IParking> GetEnumerator()
        {
            return Transports.GetEnumerator();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Globalization;
namespace GetData
{
    class Transport 
    {
        const string Path = @"../../../..\GetData\TempData\BikesAdresses.txt";
        List<IParking> Transports;
        CoordinatesFinder coordinatesFinder;
        public Transport()
        {
            coordinatesFinder = new CoordinatesFinder();
            Transports = new List<IParking>();
            GetCoordinates();
        }
        private void GetCoordinates()
        {
            using (StreamReader TextFileStream = new StreamReader(Path))
            {
                string line;
                while ((line = TextFileStream.ReadLine()) != null)
                {
                    string[] temp = coordinatesFinder.GetCoordinates(line);
                    try
                    {
                        float longtitude = float.Parse(temp[0], CultureInfo.InvariantCulture);
                        float latitude = float.Parse(temp[1], CultureInfo.InvariantCulture);
                        Transports.Add(new Parking()
                        {
                            Longitude = longtitude,
                            Latitude = latitude,


                        });
                    }
                    catch { }
                    
                    
                }
            }
        }

        public IEnumerator<IParking> GetEnumerator()
        {
            return Transports.GetEnumerator();
        }
    }
}

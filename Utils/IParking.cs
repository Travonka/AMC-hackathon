using System;
using System.Collections.Generic;
using System.Text;

namespace GetData
{
    public interface IParking
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public enum TransportType { }
    }
}

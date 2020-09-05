using System;
using System.Collections.Generic;
using System.Text;

namespace GetData
{
    interface ITransport
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public enum TransportType { }


    }
}

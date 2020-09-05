
namespace GetData
{
    public interface ITransport
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public enum TransportType { }
    }
}

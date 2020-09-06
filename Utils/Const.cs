namespace Utils
{
    public static class Const
    {
        public const string URL_OSM_DATA = "https://download.geofabrik.de/russia/central-fed-district-latest.osm.pbf";
        public const string URL_INFECTED = "https://coronavirus-online.moscow/sluchai-koronavirusa-v-moskve/";

        private const string PATH_MAP_PREFIX = "../MapData/";
        public const string PATH_TO_OSMFBF = PATH_MAP_PREFIX + "central-district-raw.osm.pbf";
        public const string PATH_TO_SERIALIZED = PATH_MAP_PREFIX + "serialized";
        public const string PATH_TO_INFECTED_JSON = PATH_MAP_PREFIX + "infected.json";
        public const string PATH_TO_YANDEX_COORDINATES_CACHE = PATH_MAP_PREFIX + "yandex_cache.json";

        public const float LATITUDE_UP = 55.95f;
        public const float LATITUDE_DOWN = 55.55f;
        public const float LONGITUDE_UP = 38.01f;
        public const float LONGITUDE_DOWN = 37.17f;
    }
}

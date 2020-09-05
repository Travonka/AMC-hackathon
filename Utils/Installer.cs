using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Const
    {
        public const string DOWNLOAD_URL = "https://download.geofabrik.de/russia/central-fed-district-latest.osm.pbf";
        public const string PATH_TO_OSMFBF = @"../../../../MapData/central-district-raw.osm.pbf";
        public const string PATH_TO_SERIALIZED = @"../../../../MapData/serialized";
    }

    public static class Installer
    {
        private static bool Download(string url, string destinationPath)
        {
            if (File.Exists(destinationPath))
                return false;
            using (var wc = new WebClient())
            {
                wc.DownloadFile(new Uri(url), destinationPath);
            }
            return true;
        }

        private static bool Serialize(string from, string to)
        {
            if (File.Exists(to))
                return false;
            var routerDb = new RouterDb();
            using (var stream = new FileInfo(from).OpenRead())
            {
                routerDb.LoadOsmData(stream);
            }

            using (var stream = new FileInfo(to).Open(FileMode.Create))
            {
                routerDb.Serialize(stream);
            }

            return true;
        }

        public static void InstallAndLog()
        {
            var sw = new Stopwatch();
            Console.WriteLine("Installing begins");
            
            sw.Start();
            if (Download(Const.DOWNLOAD_URL, Const.PATH_TO_OSMFBF))
                Console.WriteLine($"Spent {sw.ElapsedMilliseconds} ms on downloading");
            else
                Console.WriteLine("Downloading skipped");
            sw.Reset();
            sw.Start();
            if (Serialize(Const.PATH_TO_OSMFBF, Const.PATH_TO_SERIALIZED))
                Console.WriteLine($"Spent {sw.ElapsedMilliseconds} ms on serialization");
            else
                Console.WriteLine("Serialization skipped");
            
            Console.WriteLine("OK");
        }
    }
}

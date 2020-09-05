﻿using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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

        public enum LogLevel 
        {
            EVERYTHING = 0, ANY_INFO = 0,
            ACTION = 1,     IMPORTANT_INFO = 1,
            NONE = 2,       
        }

        public static void Install(LogLevel logLevel)
        {
            void Log(string msg, LogLevel level)
            {
                if ((int)level >= (int)logLevel)
                    Console.WriteLine($"Installer: {msg}");
            }

            var sw = new Stopwatch();
            
            sw.Start();
            if (Download(Const.DOWNLOAD_URL, Const.PATH_TO_OSMFBF))
            {
                Log("Installing begins", LogLevel.IMPORTANT_INFO);
                Log($"Spent {sw.ElapsedMilliseconds} ms on downloading", LogLevel.IMPORTANT_INFO);
            }
            else
                Log("Downloading skipped", LogLevel.ANY_INFO);
            sw.Reset();
            sw.Start();
            if (Serialize(Const.PATH_TO_OSMFBF, Const.PATH_TO_SERIALIZED))
            {
                Log("Serialization begins", LogLevel.IMPORTANT_INFO);
                Log($"Spent {sw.ElapsedMilliseconds} ms on serialization", LogLevel.IMPORTANT_INFO);
            }
            else
                Log("Serialization skipped", LogLevel.ANY_INFO);

            Log("OK", LogLevel.ANY_INFO);
        }
    }
}

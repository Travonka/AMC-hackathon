using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Reflection.Metadata;
using Utils;

namespace GetData
{
    public sealed class YandexAPIRequester
    {
        private Dictionary<string, string> data;
        private string path;
        public YandexAPIRequester(string path)
        {
            this.path = path;
            data = File.Exists(path) ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(path))
                : new Dictionary<string, string>();
        }

        private void UpdateCache()
        {
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(path, json);
        }

        public string GetResponse(string url)
        {
            if (!data.ContainsKey(url))
            {
                var stream = WebRequest.Create(url).GetResponse().GetResponseStream();
                var reader = new StreamReader(stream);
                string line;
                var lines = new List<string>();
                while ((line = reader.ReadLine()) is { })
                    lines.Add(line);
                data[url] = string.Join('\n', lines);
                UpdateCache();
            }
            return data[url];
        }
    }

    public sealed class CoordinatesFinder
    {
        const string ApiKey = "d58c5729-9450-4c4b-805b-5d5a959a82e7";
        private YandexAPIRequester requester;

        public CoordinatesFinder()
        {
            requester = new YandexAPIRequester(Const.PATH_TO_YANDEX_COORDINATES_CACHE);
        }

        public (float longitude, float latitude) GetCoordinates(string adress)
        {
            var responseRaw = requester.GetResponse($"https://geocode-maps.yandex.ru/1.x?geocode={adress}&apikey={ApiKey}&format=json");
            var a = 4;
            return (0, 0);
        }
        private string wheelchair(string text)
        {
            
           List<string> listofwords = text.Split('"').ToList();
           int index =  listofwords.IndexOf("pos");
           index += 2;
           return listofwords[index];
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Utils
{
    public sealed class InfectedBuilding
    {
        public string Street { get; set; }
        public string Building { get; set; }
    }
    
    public static class InfectedBuildingsLoader
    {
        public static List<InfectedBuilding> Load()
        {
            if (!File.Exists(Const.PATH_TO_INFECTED_JSON))
                FromSiteToJson(Const.URL_INFECTED, Const.PATH_TO_INFECTED_JSON);
            return JsonSerializer.Deserialize<List<InfectedBuilding>>(File.ReadAllText(Const.PATH_TO_INFECTED_JSON));
        }

        private static void FromSiteToJson(string urlInfected, string pathToSave)
        {
            var webClient = new WebClient();
            var page = webClient.DownloadString(urlInfected);

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            var table = doc.DocumentNode.SelectSingleNode("//table[@class='tablepress tablepress-id-6']")
                        .Descendants("tr")
                        .Skip(1)
                        .Where(tr => tr.Elements("td").Count() > 1)
                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                        .ToList();
            
            var infectedBuildings = new List<InfectedBuilding>();
            for (int i =0; i< table.Count; i++)
                infectedBuildings.Add(new InfectedBuilding() { Street = table[i][0], Building = table[i][1] });

            var json = JsonSerializer.Serialize(infectedBuildings);
            
            File.WriteAllText(pathToSave, json);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
//Install-Package HtmlAgilityPack -Version 1.11.24

namespace ConsoleApp4
{
    class InfectedBuilding
    {
        public string Street { get; set; }
        public string Building { get; set; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("https://coronavirus-online.moscow/sluchai-koronavirusa-v-moskve/");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='tablepress tablepress-id-6']")
                        .Descendants("tr")
                        .Skip(1)
                        .Where(tr => tr.Elements("td").Count() > 1)
                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                        .ToList();
            
            List<InfectedBuilding> infectedBuildings = new List<InfectedBuilding>();
            for (int i =0; i< table.Count; i++)
            {
                infectedBuildings.Add(new InfectedBuilding() { Street = table[i][0], Building = table[i][1] });
            }

            string json = JsonSerializer.Serialize(infectedBuildings);
            var s = JsonSerializer.Deserialize<List<InfectedBuilding>>(json);
            for (int i = 0; i < table.Count; i++)
            {
                Console.WriteLine(s[i].Street + " " + s[i].Building );
            }
        }
    }
}

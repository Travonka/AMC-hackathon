using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using System.Linq;

namespace GetData
{
    class CoordinatesFinder
    {
        const string ApiKey = "d58c5729-9450-4c4b-805b-5d5a959a82e7";
        public string[] GetCoordinates(string adress)
        {
            string[] Coor = new string[2];
            WebRequest request = WebRequest.Create($"https://geocode-maps.yandex.ru/1.x?geocode={adress}&apikey={ApiKey}&format=json");
            WebResponse Response = request.GetResponse();
            using (Stream stream = Response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        var temp = wheelchair(line).Split() ;
                        try
                        {
                            Coor[0] = temp[0];
                            Coor[1] = temp[1];
                        }
                        catch { }
                    }
                }
            }
            return Coor;
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

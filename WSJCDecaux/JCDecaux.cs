using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSJCDecaux
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class JCDecaux : IJCDecaux
    {
        public static JArray apiRequest(string url)
        {
            Console.WriteLine("Getting data...");
            Console.WriteLine();
            WebRequest request = WebRequest.Create(url);
            WebResponse response;
            try
            {
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                JArray res = JArray.Parse(responseFromServer);
                reader.Close();
                response.Close();
                return res;
            }
            catch (WebException e)
            {
                response = e.Response;
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                dynamic res = JObject.Parse(responseFromServer);
                reader.Close();
                response.Close();
                Console.WriteLine(res.error);
                return new JArray();
            }

        }

        public Station[] GetStations(string city)
        {
            JArray res = apiRequest("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=54624b5946c8283382dda67afe674be570af109f");
            IList<JToken> results = res.ToList();
            Station[] stations = new Station[results.Count];
            for(int i=0; i < stations.Length; i++)
            {
                stations[i] = results[i].ToObject<Station>();
            }
            return stations;
        }

    }
}

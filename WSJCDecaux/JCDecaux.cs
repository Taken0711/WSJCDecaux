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

        private Dictionary<string, Station[]> cache = new Dictionary<string, Station[]>();
        private Dictionary<string, DateTime> timestamps = new Dictionary<string, DateTime>();

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

        public string[] GetContracts()
        {
            Console.WriteLine("Getting contracts...");
            JArray res = apiRequest("https://api.jcdecaux.com/vls/v1/contracts?apiKey=54624b5946c8283382dda67afe674be570af109f");
            IList<JToken> results = res.ToList();
            string[] stations = new string[results.Count];
            for (int i = 0; i < stations.Length; i++)
            {
                stations[i] = (string)results[i]["name"];
            }
            Console.WriteLine("Return contracts from JCDecaux");
            return stations;
        }

        public Station[] GetStations(string contract, int timeout)
        {
            Console.WriteLine($"Getting stations for contract {contract}...");
            if (!(cache.ContainsKey(contract) && System.DateTime.Now < timestamps[contract].AddSeconds(timeout)))
            {
                JArray res = apiRequest("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=54624b5946c8283382dda67afe674be570af109f");
                timestamps[contract] = System.DateTime.Now;
                IList<JToken> results = res.ToList();
                cache[contract] = new Station[results.Count];
                for (int i = 0; i < cache[contract].Length; i++)
                {
                    cache[contract][i] = results[i].ToObject<Station>();
                }
                Console.WriteLine("Returning stations for contract {contract} from JCDecaux");
            } else
            {
                Console.WriteLine("Returning stations for contract {contract} from cache");
            }
            return cache[contract];
        }

    }
}

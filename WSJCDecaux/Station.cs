using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WSJCDecaux
{
    [DataContract]
    public class Station
    {
        [JsonProperty("name")]
        string name;
        [JsonProperty("address")]
        string address;
        [JsonProperty("bike_stands")]
        int bikeStands;
        [JsonProperty("available_bike_stands")]
        int availableBikeStands;
        [JsonProperty("available_bikes")]
        int availableBikes;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        [DataMember]
        public int BikeStands
        {
            get { return bikeStands; }
            set { bikeStands = value; }
        }
        [DataMember]
        public int AvailableBikeStands
        {
            get { return availableBikeStands; }
            set { availableBikeStands = value; }
        }
        [DataMember]
        public int AvailableBikes
        {
            get { return availableBikes; }
            set { availableBikes = value; }
        }
    }
}

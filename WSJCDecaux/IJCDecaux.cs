using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSJCDecaux
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IJCDecaux
    {
        [OperationContract]
        Station[] GetStations(string contract, int timeout);
        [OperationContract]
        string[] GetContracts();

    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "WSJCDecaux.ContractType".
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

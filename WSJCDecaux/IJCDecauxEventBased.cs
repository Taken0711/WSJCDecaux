using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSJCDecaux
{
    [ServiceContract(CallbackContract = typeof(IJCDecauxEventBasedEvents))]
    public interface IJCDecauxEventBased
    {
        [OperationContract]
        void SubscribeStationChanged(string contract, string station, int timer);
        [OperationContract]
        Station[] GetStations(string contract, int timeout);
        [OperationContract]
        string[] GetContracts();
    }
}

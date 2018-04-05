using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSJCDecaux
{
    public interface IJCDecauxEventBasedEvents
    {
        [OperationContract(IsOneWay = true)]
        void StationChanged(Station station);
    }
}

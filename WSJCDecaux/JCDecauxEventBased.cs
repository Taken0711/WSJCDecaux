using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WSJCDecaux
{
    public class JCDecauxEventBased : AbstractJCDecaux, IJCDecauxEventBased
    {
        private Dictionary<string, Dictionary<string, Action<Station>>> subscribers = new Dictionary<string, Dictionary<string, Action<Station>>>();

        public void SubscribeStationChanged(string contract, string station)
        {
            IJCDecauxEventBasedEvents subscriber = OperationContext.Current.GetCallbackChannel<IJCDecauxEventBasedEvents>();
            if (!subscribers.ContainsKey(contract))
                subscribers[contract] = new Dictionary<string, Action<Station>>();
            if (!subscribers[contract].ContainsKey(station))
                subscribers[contract][station] = delegate { };
            subscribers[contract][station] += subscriber.StationChanged;
        }
    }
}

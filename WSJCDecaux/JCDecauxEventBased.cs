using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace WSJCDecaux
{
    public class JCDecauxEventBased : AbstractJCDecaux, IJCDecauxEventBased
    {
        private Dictionary<string, Dictionary<string, Action<Station>>> subscribers = new Dictionary<string, Dictionary<string, Action<Station>>>();

        public void SubscribeStationChanged(string contract, string station, int timer)
        {
            /*IJCDecauxEventBasedEvents subscriber = OperationContext.Current.GetCallbackChannel<IJCDecauxEventBasedEvents>();
            if (!subscribers.ContainsKey(contract))
                subscribers[contract] = new Dictionary<string, Action<Station>>();
            if (!subscribers[contract].ContainsKey(station))
                subscribers[contract][station] = delegate { };
            subscribers[contract][station] += subscriber.StationChanged;*/
            IJCDecauxEventBasedEvents subscriber = OperationContext.Current.GetCallbackChannel<IJCDecauxEventBasedEvents>();
            Task.Run(() => {
                while(true)
                {
                    foreach (Station s in GetStations(contract, 0))
                    {
                        if (s.Name.Contains(station))
                        {
                            subscriber.StationChanged(s);
                            break;
                        }
                    }
                    Thread.Sleep(timer * 1000);
                }
            });
        }
    }
}

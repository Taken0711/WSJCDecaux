using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using EventBasedClient.JCDecauxEventBased;
using WpfApp1.JCDecaux;

namespace EventBasedClient
{
    class StationComparer : IComparer<Station>
    {
        public int Compare(Station x, Station y)
        {
            try
            {
                Func<Station, int> getIndex = t => int.Parse(t.Name.Split('-')[0]);
                return getIndex(x).CompareTo(getIndex(y));
            }
            catch
            {
                return string.Compare(x.Name, y.Name);
            }

        }
    }

    class Program : JCDecauxEventBased.IJCDecauxEventBasedCallback
    {
        Dictionary<string, Tuple<Func<List<string>, bool>, string, string>> commands = new Dictionary<string, Tuple<Func<List<string>, bool>, string, string>>();
        JCDecauxEventBasedClient client;
        List<Station> stationList;

        public Program()
        {
            RegisterCommand("exit", Exit, "exit", "exit the client");
            RegisterCommand("help", Help, "help", "display this");
            RegisterCommand("list-contracts", ListContracts, "list-contracts", "list all the contracts");
            RegisterCommand("list-stations", ListStations, "list-stations CONTRACT [NAME_FILTERS...]", "list the stations filtered by name and its available bike for a contract");
            RegisterCommand("subscribe", SubscribeStations, "subscribe CONTRACT [NAME_FILTERS...]", "subscribe to stations filtered by name and its available bike for a contract");
            InstanceContext iCntxt = new InstanceContext(this);
            client = new JCDecauxEventBasedClient(iCntxt, "EB");
        }

        private void RegisterCommand(string name, Func<List<string>, bool> act, string usage, string desc)
        {
            commands.Add(name, Tuple.Create(act, usage, desc));
        }

        public bool Help(List<string> args)
        {
            Console.WriteLine("Available commands:");
            foreach (KeyValuePair<string, Tuple<Func<List<string>, bool>, string, string>> entry in commands)
            {
                Console.WriteLine($"\t{entry.Value.Item2} - {entry.Value.Item3}");
            }
            return true;
        }

        public bool Exit(List<string> args)
        {
            return false;
        }

        public bool ListContracts(List<string> args)
        {
            string[] contracts = client.GetContracts();
            Console.WriteLine(string.Join(", ", contracts));
            return true;
        }

        public bool ListStations(List<string> args)
        {
            if (args.Count < 1)
            {
                Console.WriteLine($"Not enough arguments. Usage: {commands["list-stations"].Item2}");
                return true;
            }
            Station[] stations = client.GetStations(args[0], 180);
            stationList = stations.ToList<Station>();
            stationList.Sort(new StationComparer());
            IEnumerable<Station> tmp;
            if (args.Count < 2)
                tmp = stationList;
            else
            {
                string filter = string.Join(" ", args.Skip(1));
                tmp = stationList.Where(s => s.Name.Contains(filter));
            }
            Console.WriteLine(string.Join(Environment.NewLine, tmp.Select(s => $"\t{s.Name} - Bike stands: {s.BikeStands}, Available bike stands: {s.AvailableBikeStands}, Available bikes: {s.AvailableBikes}")));
            return true;
        }

        public bool SubscribeStations(List<string> args)
        {
            if (args.Count < 1)
            {
                Console.WriteLine($"Not enough arguments. Usage: {commands["subscribe"].Item2}");
                return true;
            }
            foreach (string s in args)
            {
                client.SubscribeStationChanged(args[0], s, 10);
            }
            return true;
        }

        public bool Eval(string cmd)
        {
            try
            {
                string[] args = cmd.Split(' ');
                return commands[args[0]].Item1.Invoke(args.Skip(1).ToList());
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Unknown command '{cmd}'.");
                return Help(null);
            }
        }

        public void StationChanged(Station s)
        {
            Console.WriteLine("");
            Console.WriteLine($"Update for station '{s.Name}' -- Bike stands: {s.BikeStands}, Available bike stands: {s.AvailableBikeStands}, Available bikes: {s.AvailableBikes}");
            Console.Write(">>> ");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("CLI Client for JCDecaux service.");
            Console.WriteLine("Type 'help' to see available commands.");
            bool shouldContinue = true;
            while (shouldContinue)
            {
                Console.Write(">>> ");
                shouldContinue = p.Eval(Console.ReadLine());
            }
        }
    }
}

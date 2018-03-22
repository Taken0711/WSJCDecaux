using CLIClient.JCDecaux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIClient
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

    class Program
    {
        Dictionary<string, Tuple<Func<List<string>, bool>, string, string>> commands = new Dictionary<string, Tuple<Func<List<string>, bool>, string, string>>();
        JCDecauxClient client;
        List<Station> stationList;

        public Program()
        {
            RegisterCommand("exit", Exit, "exit", "exit the client");
            RegisterCommand("help", Help, "help", "display this");
            RegisterCommand("list-contracts", ListContracts, "list-contracts", "list all the contracts");
            RegisterCommand("list-stations", ListStations, "list-stations CONTRACT [NAME_FILTERS...]", "list the stations filtered by name and its available bike for a contract");
            client = new JCDecauxClient("IJCDecaux");

        }

        private void RegisterCommand(string name, Func<List<string>, bool> act, string usage, string desc)
        {
            commands.Add(name, Tuple.Create(act, usage, desc));
        }

        public bool Help(List<string> args)
        {
            Console.WriteLine("Available commands:");
            foreach(KeyValuePair<string, Tuple<Func<List<string>, bool>, string, string>> entry in commands) {
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
            Station[] stations = client.GetStations(args[0]);
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

        public bool Eval(string cmd)
        {
            try
            {
                string[] args = cmd.Split(' ');
                return commands[args[0]].Item1.Invoke(args.Skip(1).ToList());
            } catch(KeyNotFoundException)
            {
                Console.WriteLine($"Unknown command '{cmd}'.");
                return Help(null);
            }
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

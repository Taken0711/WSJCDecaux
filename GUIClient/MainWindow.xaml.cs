using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.JCDecaux;

namespace Client
{

    class StationComparer : IComparer<Station>
    {
        public int Compare(Station x, Station y)
        {
            try
            {
                Func<Station, int> getIndex = t => int.Parse(t.Name.Split('-')[0]);
                return getIndex(x).CompareTo(getIndex(y));
            } catch
            {
                return string.Compare(x.Name, y.Name);
            }
            
        }
    }

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<string> contractList = new ObservableCollection<string>();
        private ObservableCollection<string> stationList = new ObservableCollection<string>();
        private List<Station> contractStations;
        private string selectedContract;
        private string selectedStation;

        public MainWindow()
        {
            InitializeComponent();
            this.contracts.SelectionChanged += new SelectionChangedEventHandler(ContractChanged);
            this.contracts.ItemsSource = contractList;
            this.stations.SelectionChanged += new SelectionChangedEventHandler(StationChanged);
            this.stations.ItemsSource = stationList;
            GetContracts();
        }

        private void GettingData()
        {
            this.resultBox.Text = "Getting data...";
            this.progress.Value = 33;
        }

        private void ContractChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
                return;
            string contract = (string) e.AddedItems[0];
            this.selectedContract = contract;
            this.stations.SelectedIndex = -1;
            GetStations(contract);
        }

        private void StationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
                return;
            string station = (string)e.AddedItems[0];
            foreach(Station s in contractStations)
            {
                if(s.Name == station)
                {
                    this.resultBox.Text = $"Bike stands: {s.BikeStands}{Environment.NewLine}Available bike stands: {s.AvailableBikeStands}{Environment.NewLine}Available bikes: {s.AvailableBikes}";
                    break;
                }
            }
        }

        async void GetContracts()
        {
            GettingData();
            contractList.Clear();
            stationList.Clear();
            JCDecauxClient client = new JCDecauxClient("IJCDecaux");
            string[] contracts = await client.GetContractsAsync();
            this.progress.Value = 66;
            List<string> l = contracts.ToList<string>();
            l.Sort();
            foreach(string s in l)
            {
                contractList.Add(s);
            }
            this.resultBox.Text = "Select a contract";
            this.progress.Value = 100;
        }

        async void GetStations(string contract)
        {
            GettingData();
            stationList.Clear();
            List<Station> l;
            JCDecauxClient client = new JCDecauxClient("IJCDecaux");
            Station[] stations = await client.GetStationsAsync(contract);
            l = stations.ToList<Station>();
            this.progress.Value = 66;
            await Task.Run(() =>
            {
                l.Sort(new StationComparer());
            });

            contractStations = l;
            foreach (Station s in l)
            {
                stationList.Add(s.Name);
            }
            this.resultBox.Text = "Select a station";
            this.progress.Value = 100;
        }

        private int GetStationIndex(Station s)
        {
            string res = s.Name.Split('-')[0];
            return int.Parse(res);
        }
    }
}

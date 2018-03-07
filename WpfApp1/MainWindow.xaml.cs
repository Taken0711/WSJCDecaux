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
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<string> contractList = new ObservableCollection<string>();
        private ObservableCollection<string> stationList = new ObservableCollection<string>();
        private Dictionary<string, List<Station>> cache = new Dictionary<string, List<Station>>();

        public MainWindow()
        {
            InitializeComponent();
            this.contracts.SelectionChanged += new SelectionChangedEventHandler(ContractChanged);
            this.contracts.ItemsSource = contractList;
            this.stations.ItemsSource = stationList;
            GetContracts();
        }

        private void ContractChanged(object sender, SelectionChangedEventArgs e)
        {
            string contract = (string) e.AddedItems[0];
            GetStations(contract);
        }

        async void GetContracts()
        {
            JCDecauxClient client = new JCDecauxClient("IJCDecaux");
            string[] contracts = await client.GetContractsAsync();
            List<string> l = contracts.ToList<string>();
            l.Sort();
            contractList.Clear();
            foreach(string s in l)
            {
                contractList.Add(s);
            }
        }

        async void GetStations(string contract)
        {
            JCDecauxClient client = new JCDecauxClient("IJCDecaux");
            Station[] stations = await client.GetStationsAsync(contract);
            List<Station> l = stations.ToList<Station>();
            l.OrderBy(o => o.Name);
            cache[contract] = l;
            stationList.Clear();
            foreach (Station s in l)
            {
                stationList.Add(s.Name);
            }
        }
    }
}

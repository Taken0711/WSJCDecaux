using System;
using System.Collections.Generic;
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
using GUIClient.MathsLibrary;

namespace GUIClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MathsOperationsClient client;

        public MainWindow()
        {
            InitializeComponent();
            this.binding.Items.Add("BasicHttpBinding_IMathsOperations");
            this.binding.Items.Add("WSHttpBinding_IMathsOperations");
            this.binding.SelectedIndex = 0;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            this.res.Text = client.Add(int.Parse(this.num1.Text), int.Parse(this.num2.Text)).ToString();
        }

        private void Mult(object sender, RoutedEventArgs e)
        {
            this.res.Text = client.Multiply(int.Parse(this.num1.Text), int.Parse(this.num2.Text)).ToString();
        }

        private void Div(object sender, RoutedEventArgs e)
        {
            this.res.Text = client.Divide(int.Parse(this.num1.Text), int.Parse(this.num2.Text)).ToString();
        }

        private void binding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            client = new MathsOperationsClient((string) e.AddedItems[0]);
        }
    }
}

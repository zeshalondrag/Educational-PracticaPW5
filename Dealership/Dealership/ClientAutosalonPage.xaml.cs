using Dealership.DealershipDataSetTableAdapters;
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

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для ClientAutosalonPage.xaml
    /// </summary>
    public partial class ClientAutosalonPage : Page
    {
        DealershipCarsTableAdapter dealershipCars = new DealershipCarsTableAdapter();
        public ClientAutosalonPage()
        {
            InitializeComponent();
            dealershipCarsDgr.ItemsSource = dealershipCars.GetDataBy3();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dealershipCarsDgr.Columns[0].Visibility = Visibility.Collapsed;
            dealershipCarsDgr.Columns[1].Visibility = Visibility.Collapsed;
            dealershipCarsDgr.Columns[2].Visibility = Visibility.Collapsed;
            dealershipCarsDgr.Columns[3].Header = "Название автосалона";
            dealershipCarsDgr.Columns[4].Header = "Машина";
        }
    }
}

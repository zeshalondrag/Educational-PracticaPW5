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
    /// Логика взаимодействия для ClientCarsPage.xaml
    /// </summary>
    public partial class ClientCarsPage : Page
    {
        CarsTableAdapter cars = new CarsTableAdapter();
        public ClientCarsPage()
        {
            InitializeComponent();
            carsDgr.ItemsSource = cars.GetDataBy1();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            carsDgr.Columns[0].Visibility = Visibility.Collapsed;
            carsDgr.Columns[1].Visibility = Visibility.Collapsed;
            carsDgr.Columns[2].Visibility = Visibility.Collapsed;
            carsDgr.Columns[3].Visibility = Visibility.Collapsed;
            carsDgr.Columns[4].Header = "Год выпуска";
            carsDgr.Columns[5].Header = "Цена";
            carsDgr.Columns[6].Header = "Бренд авто";
            carsDgr.Columns[7].Header = "Модель авто";
            carsDgr.Columns[8].Header = "Мощность двигателя";
            carsDgr.Columns[9].Header = "Объём двигателя";
            carsDgr.Columns[10].Header = "Привод";
        }
    }
}
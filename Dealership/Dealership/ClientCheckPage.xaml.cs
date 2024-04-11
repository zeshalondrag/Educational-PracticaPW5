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
    /// Логика взаимодействия для ClientCheckPage.xaml
    /// </summary>
    public partial class ClientCheckPage : Page
    {
        OrdersChecksTableAdapter orderschecks = new OrdersChecksTableAdapter();
        public ClientCheckPage()
        {
            InitializeComponent();
            ordersChecksDgr.ItemsSource = orderschecks.GetDataBy3();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ordersChecksDgr.Columns[0].Visibility = Visibility.Collapsed;
            ordersChecksDgr.Columns[1].Visibility = Visibility.Collapsed;
            ordersChecksDgr.Columns[2].Visibility = Visibility.Collapsed;
            ordersChecksDgr.Columns[3].Visibility = Visibility.Collapsed;
            ordersChecksDgr.Columns[4].Header = "Цена";
            ordersChecksDgr.Columns[5].Header = "Фамилия клиента";
            ordersChecksDgr.Columns[6].Header = "Фамидия сотрудника";
            ordersChecksDgr.Columns[7].Header = "Название автосалона";
            ordersChecksDgr.Columns[8].Header = "Бренд авто";
            ordersChecksDgr.Columns[9].Header = "Количество";
            ordersChecksDgr.Columns[10].Header = "Дата заказа";
        }
    }
}
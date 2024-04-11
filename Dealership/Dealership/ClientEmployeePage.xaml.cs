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
    /// Логика взаимодействия для ClientEmployeePage.xaml
    /// </summary>
    public partial class ClientEmployeePage : Page
    {
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        public ClientEmployeePage()
        {
            InitializeComponent();
            employeesDgr.ItemsSource = employees.GetData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            employeesDgr.Columns[0].Visibility = Visibility.Collapsed;
            employeesDgr.Columns[5].Visibility = Visibility.Collapsed;
            employeesDgr.Columns[6].Visibility = Visibility.Collapsed;

            employeesDgr.Columns[1].Header = "Фамилия";
            employeesDgr.Columns[2].Header = "Имя";
            employeesDgr.Columns[3].Header = "Отчество";
            employeesDgr.Columns[4].Header = "Номер телефона";

        }
    }
}
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
using System.Windows.Shapes;
using Dealership.DealershipDataSetTableAdapters;

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        List<string> nameTable = new List<string> { "Сотрудники", "Чек", "Автосалон", "Машины" };
        public Client()
        {
            InitializeComponent();
            selecttablesCbx.ItemsSource = nameTable;

            MinHeight = 450;
            MinWidth = 800;
        }

        private void selecttablesCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = selecttablesCbx.SelectedItem.ToString();

            if (nameTable == "Сотрудники")
            {
                TablesPage.Content = new ClientEmployeePage();
            }
            else if (nameTable == "Чек")
            {
                TablesPage.Content = new ClientCheckPage();
            }
            else if (nameTable == "Автосалон")
            {
                TablesPage.Content = new ClientAutosalonPage();
            }
            else if (nameTable == "Машины")
            {
                TablesPage.Content = new ClientCarsPage();
            } 
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow clients = new MainWindow();
            clients.Show();
            Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml;
using Dealership.DealershipDataSetTableAdapters;

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        QueriesTableAdapter backups = new QueriesTableAdapter();
        List<string> nameTable = new List<string> { "Авторизация", "Клиенты", "Сотрудники", "Должности", "Заказы", "Чек", "Статус", "Автосалон-Машина", "Автосалон", "Машины", "Бренды", "Модели", "Характеристики" };
        public Administrator()
        {
            InitializeComponent();
            selecttablesCbx.ItemsSource = nameTable;

            MinHeight = 450;
            MinWidth = 800;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow authorization = new MainWindow();
            authorization.Show();
            Close();
        }

        private void selecttablesCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameTable = selecttablesCbx.SelectedItem.ToString();

            if (nameTable == "Авторизация")
            {
                TablesPage.Content = new AuthorizationsPage();
            }
            else if (nameTable == "Клиенты")
            {
                TablesPage.Content = new ClientsPage();
            }
            else if (nameTable == "Сотрудники")
            {
                TablesPage.Content = new EmployeesPage();
            }
            else if (nameTable == "Должности")
            {
                TablesPage.Content = new PositionPage();
            }
            else if (nameTable == "Заказы")
            {
                TablesPage.Content = new OrdersPage();
            }
            else if (nameTable == "Чек")
            {
                TablesPage.Content = new OrdersСhecksPage();
            }
            else if (nameTable == "Статус")
            {
                TablesPage.Content = new StatusPage();
            }
            else if (nameTable == "Автосалон-Машина")
            {
                TablesPage.Content = new DealershipCarsPage();
            }
            else if (nameTable == "Автосалон")
            {
                TablesPage.Content = new DealershipPage();
            }
            else if (nameTable == "Машины")
            {
                TablesPage.Content = new CarsPage();
            }
            else if (nameTable == "Бренды")
            {
                TablesPage.Content = new BrandsPage();
            }
            else if (nameTable == "Модели")
            {
                TablesPage.Content = new ModelsPage();
            }
            else if (nameTable == "Характеристики")
            {
                TablesPage.Content = new SpecificationsPage();
            }
        }

        private void backup_Click(object sender, RoutedEventArgs e)
        {
            backups.Backup_Dealership();
            MessageBox.Show("Бекап успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
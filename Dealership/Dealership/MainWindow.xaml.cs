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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dealership.DealershipDataSetTableAdapters;

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthorizationsTableAdapter auth = new AuthorizationsTableAdapter();
        ClientsTableAdapter clients = new ClientsTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var allLogins = auth.GetData().Rows;

                foreach (DataRow row in allLogins)
                {
                    if (row["LoginA"].ToString() == LoginTbx.Text && row["PasswordA"].ToString() == PasswordTbx.Password)
                    {
                        int authorizationsID = (int)row["ID_Authorizations"];

                        DataRow[] clientRow = clients.GetData().Select("Authorizations_ID = " + authorizationsID);
                        if (clientRow.Length > 0)
                        {
                            Client client = new Client();
                            client.Show();
                            Close();
                            return;
                        }

                        DataRow[] employeeRow = employees.GetData().Select("Authorizations_ID = " + authorizationsID);
                        if (employeeRow.Length > 0)
                        {
                            Administrator administrator = new Administrator();
                            administrator.Show();
                            Close();
                            return;
                        }

                        MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                MessageBox.Show("Неверные логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
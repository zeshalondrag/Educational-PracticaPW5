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
using System.IO;
using Dealership.DealershipDataSetTableAdapters;

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для OrdersСhecksPage.xaml
    /// </summary>
    public partial class OrdersСhecksPage : Page
    {
        OrdersChecksTableAdapter orderschecks = new OrdersChecksTableAdapter();
        ClientsTableAdapter clients = new ClientsTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        DealershipTableAdapter dealership = new DealershipTableAdapter();
        BrandsTableAdapter brands = new BrandsTableAdapter();
        public OrdersСhecksPage()
        {
            InitializeComponent();
            ordersChecksDgr.ItemsSource = orderschecks.GetDataBy3();
            clientCbx.ItemsSource = clients.GetData();
            clientCbx.DisplayMemberPath = "ClientSurname";
            employeeCbx.ItemsSource = employees.GetData();
            employeeCbx.DisplayMemberPath = "EmployeeSurname";
            autosalonCbx.ItemsSource = dealership.GetData();
            autosalonCbx.DisplayMemberPath = "DealershipName";
            brandCbx.ItemsSource = brands.GetData();
            brandCbx.DisplayMemberPath = "BrandName";
        }

        private void ordersChecksDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ordersChecksDgr.SelectedItem != null)
            {
                DataRowView row = ordersChecksDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    clientCbx.Text = row.Row["ClientSurname"].ToString();
                    employeeCbx.Text = row.Row["EmployeeSurname"].ToString();
                    autosalonCbx.Text = row.Row["DealershipName"].ToString();
                    brandCbx.Text = row.Row["BrandName"].ToString();
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clientCbx.SelectedItem == null || employeeCbx.SelectedItem == null || autosalonCbx.SelectedItem == null || brandCbx.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (clientCbx.SelectedItem is DataRowView selectedclient && employeeCbx.SelectedItem is DataRowView selectedemployee)
                {
                    int selectedclientId = Convert.ToInt32(selectedclient["ID_Client"]);
                    int selectedemployeeid = Convert.ToInt32(selectedemployee["ID_Employee"]);
                    //orderschecks.InsertQuery(selectedclientId, selectedemployeeid, text3.Text, Convert.ToDecimal(text4.Text));
                    ordersChecksDgr.ItemsSource = orderschecks.GetDataBy3();
                }
            }
            catch 
            {
                MessageBox.Show("Невозможно добавить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            SetColumnsVisibilityAndHeaders();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ordersChecksDgr.SelectedItem == null)
                {
                    MessageBox.Show("Выберите элемент для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (clientCbx.SelectedItem == null || employeeCbx.SelectedItem == null || autosalonCbx.SelectedItem == null || brandCbx.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед редактированием.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                object id = (ordersChecksDgr.SelectedItem as DataRowView)?.Row[0];
                if (id != null)
                {
                    if (clientCbx.SelectedItem is DataRowView selectedclient && employeeCbx.SelectedItem is DataRowView selectedemployee)
                    {
                        int selectedclientId = Convert.ToInt32(selectedclient["ID_Client"]);
                        int selectedemployeeid = Convert.ToInt32(selectedemployee["ID_Employee"]);
                        //orderschecks.UpdateQuery(selectedclientId, selectedemployeeid, text3.Text, Convert.ToDecimal(text4.Text), Convert.ToInt32(id));
                        ordersChecksDgr.ItemsSource = orderschecks.GetDataBy3();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите элемент для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Невозможно добавить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            SetColumnsVisibilityAndHeaders();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ordersChecksDgr.SelectedItem == null)
                {
                    MessageBox.Show("Выберите элемент для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                object id = (ordersChecksDgr.SelectedItem as DataRowView)?.Row[0];
                if (id != null)
                {
                    orderschecks.DeleteQuery(Convert.ToInt32(id));
                    ordersChecksDgr.ItemsSource = orderschecks.GetDataBy3();
                }
            }
            catch
            {
                MessageBox.Show("Вы не можете удалить чек, так как он связан с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            SetColumnsVisibilityAndHeaders();
        }

        private void SetColumnsVisibilityAndHeaders()
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

        private void check_Click(object sender, RoutedEventArgs e)
        {
            if (ordersChecksDgr.Items.Count > 0)
            {
                DataRowView firstRow = ordersChecksDgr.Items[0] as DataRowView;

                if (firstRow != null)
                {
                    string clientSurname = firstRow.Row["ClientSurname"].ToString();
                    string employeeSurname = firstRow.Row["EmployeeSurname"].ToString();
                    string dealershipName = firstRow.Row["DealershipName"].ToString();
                    string brandName = firstRow.Row["BrandName"].ToString();
                    string price = firstRow.Row["Price"].ToString();
                    string amount = firstRow.Row["Amount"].ToString();
                    string date = firstRow.Row["DateOrder"].ToString();

                    Random rnd = new Random();
                    int randomCheckNumber = rnd.Next(100000, 999999);

                    string checkInfo = $"Номер чека: {randomCheckNumber}\n" +
                                       $"Фамилия клиента: {clientSurname}\n" +
                                       $"Фамилия сотрудника: {employeeSurname}\n" +
                                       $"Название автосалона: {dealershipName}\n" +
                                       $"Бренд авто: {brandName}\n" +
                                       $"Цена: {price}\n" +
                                       $"Количество: {amount}\n" +
                                       $"Дата заказа: {date}\n";

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "check.txt");

                    File.WriteAllText(filePath, checkInfo);

                    MessageBox.Show("Чек успешно выгружен на рабочий стол.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Нет данных о чеках.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Нет данных о чеках.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
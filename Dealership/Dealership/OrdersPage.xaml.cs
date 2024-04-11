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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        OrdersTableAdapter orders = new OrdersTableAdapter(); 
        DealershipTableAdapter dealership = new DealershipTableAdapter();
        BrandsTableAdapter brands = new BrandsTableAdapter();
        StatusOrdersTableAdapter statusOrders = new StatusOrdersTableAdapter();
        public OrdersPage()
        {
            InitializeComponent();
            ordersDgr.ItemsSource = orders.GetDataBy3();
            autosalonCbx.ItemsSource = dealership.GetData();
            autosalonCbx.DisplayMemberPath = "DealershipName";
            carCbx.ItemsSource = brands.GetData();
            carCbx.DisplayMemberPath = "BrandName";
            statusCbx.ItemsSource = statusOrders.GetData();
            statusCbx.DisplayMemberPath = "TypeStatus";
        }

        private void ordersDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ordersDgr.SelectedItem != null)
            {
                DataRowView row = ordersDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["Amount"].ToString();
                    autosalonCbx.Text = row.Row["DealershipName"].ToString();
                    carCbx.Text = row.Row["BrandName"].ToString();
                    statusCbx.Text = row.Row["TypeStatus"].ToString();
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (autosalonCbx.SelectedItem == null || carCbx.SelectedItem == null || statusCbx.SelectedItem == null || string.IsNullOrWhiteSpace(text1.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (autosalonCbx.SelectedItem is DataRowView selectedautosalon &&
                    carCbx.SelectedItem is DataRowView selectedbrand &&
                    statusCbx.SelectedItem is DataRowView selectedstatus)
                {
                    int selectedautosalonId = Convert.ToInt32(selectedautosalon["Dealership_ID"]);
                    int selectedbrandId = Convert.ToInt32(selectedbrand["Brand_ID"]);
                    int selectedstatusId = Convert.ToInt32(selectedstatus["StatusOrder_ID"]);

                    orders.InsertQuery(Convert.ToInt32(text1.Text), selectedautosalonId, selectedbrandId, Convert.ToString(selectedstatusId));
                    ordersDgr.ItemsSource = orders.GetDataBy3();
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
                if (autosalonCbx.SelectedItem == null || carCbx.SelectedItem == null || statusCbx.SelectedItem == null || string.IsNullOrWhiteSpace(text1.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (autosalonCbx.SelectedItem is DataRowView selectedautosalon &&
                    carCbx.SelectedItem is DataRowView selectedbrand &&
                    statusCbx.SelectedItem is DataRowView selectedstatus)
                {
                    int selectedautosalonId = Convert.ToInt32(selectedautosalon["Dealership_ID"]);
                    int selectedbrandId = Convert.ToInt32(selectedbrand["Brand_ID"]);
                    int selectedstatusId = Convert.ToInt32(selectedstatus["StatusOrder_ID"]);

                    object id = (ordersDgr.SelectedItem as DataRowView)?.Row[0];
                    if (id != null)
                    {
                        orders.UpdateQuery(Convert.ToInt32(text1.Text), selectedautosalonId, selectedbrandId, Convert.ToString(selectedstatusId), Convert.ToInt32(id));
                        ordersDgr.ItemsSource = orders.GetDataBy3();
                    }
                    else
                    {
                        MessageBox.Show("Выберите элемент для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Невозможно изменить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            SetColumnsVisibilityAndHeaders();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ordersDgr.SelectedItem == null)
                {
                    MessageBox.Show("Выберите элемент для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                object id = (ordersDgr.SelectedItem as DataRowView)?.Row[0];
                if (id != null)
                {
                    orders.DeleteQuery(Convert.ToInt32(id));
                    ordersDgr.ItemsSource = orders.GetDataBy3();
                }
            }
            catch
            {
                MessageBox.Show("Нельзя удалить так как связана с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SetColumnsVisibilityAndHeaders();
        }

        private void SetColumnsVisibilityAndHeaders()
        {
            ordersDgr.Columns[0].Visibility = Visibility.Collapsed;
            ordersDgr.Columns[1].Visibility = Visibility.Collapsed;
            ordersDgr.Columns[3].Visibility = Visibility.Collapsed;
            ordersDgr.Columns[2].Header = "Количество";
            ordersDgr.Columns[4].Header = "Дата заказа";
            ordersDgr.Columns[5].Header = "Название автосалона";
            ordersDgr.Columns[6].Header = "Бренд авто";
            ordersDgr.Columns[7].Header = "Статус";
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ordersDgr.Columns[0].Visibility = Visibility.Collapsed;
            ordersDgr.Columns[1].Visibility = Visibility.Collapsed;
            ordersDgr.Columns[3].Visibility = Visibility.Collapsed;
            ordersDgr.Columns[2].Header = "Количество";
            ordersDgr.Columns[4].Header = "Дата заказа";
            ordersDgr.Columns[5].Header = "Название автосалона";
            ordersDgr.Columns[6].Header = "Бренд авто";
            ordersDgr.Columns[7].Header = "Статус";
        }
    }
}
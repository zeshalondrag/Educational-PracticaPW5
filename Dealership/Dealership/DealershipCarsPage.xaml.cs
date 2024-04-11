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
    /// Логика взаимодействия для DealershipCarsPage.xaml
    /// </summary>
    public partial class DealershipCarsPage : Page
    {
        DealershipCarsTableAdapter dealershipCars = new DealershipCarsTableAdapter();
        DealershipTableAdapter dealership = new DealershipTableAdapter();
        BrandsTableAdapter brands = new BrandsTableAdapter();
        public DealershipCarsPage()
        {
            InitializeComponent();
            dealershipCarsDgr.ItemsSource = dealershipCars.GetDataBy3();
            autosalonCbx.ItemsSource = dealership.GetData();
            autosalonCbx.DisplayMemberPath = "DealershipName";
            carCbx.ItemsSource = brands.GetData();
            carCbx.DisplayMemberPath = "BrandName";
        }

        private void dealershipCarsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dealershipCarsDgr.SelectedItem != null)
            {
                DataRowView row = dealershipCarsDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    autosalonCbx.Text = row.Row["DealershipName"].ToString();
                    carCbx.Text = row.Row["BrandName"].ToString();
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (autosalonCbx.SelectedItem != null && carCbx.SelectedItem != null)
                {
                    DataRowView selectedautosalon = autosalonCbx.SelectedItem as DataRowView;
                    DataRowView selectedbrand = carCbx.SelectedItem as DataRowView;
                    int selectedautosalonId = Convert.ToInt32(selectedautosalon["ID_Dealership"]);
                    int selectedbrandId = Convert.ToInt32(selectedbrand["ID_Brand"]);
                    dealershipCars.InsertQuery(selectedautosalonId, selectedbrandId);
                    dealershipCarsDgr.ItemsSource = dealershipCars.GetDataBy3();
                }
                else
                {
                    MessageBox.Show("Выберите автосалон и машину для добавления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Такой автосалон и машина уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SetupDataGridColumns();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (autosalonCbx.SelectedItem != null && carCbx.SelectedItem != null && dealershipCarsDgr.SelectedItem != null)
                {
                    DataRowView selectedautosalon = autosalonCbx.SelectedItem as DataRowView;
                    DataRowView selectedbrand = carCbx.SelectedItem as DataRowView;
                    int selectedautosalonId = Convert.ToInt32(selectedautosalon["ID_Dealership"]);
                    int selectedbrandId = Convert.ToInt32(selectedbrand["ID_Brand"]);
                    object id = (dealershipCarsDgr.SelectedItem as DataRowView)?.Row[0];
                    if (id != null)
                    {
                        dealershipCars.UpdateQuery(selectedautosalonId, selectedbrandId, Convert.ToInt32(id));
                        dealershipCarsDgr.ItemsSource = dealershipCars.GetDataBy3();
                    }
                    else
                    {
                        MessageBox.Show("Выберите элемент для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите автосалон, машину и элемент для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Такой автосалон или машина уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            SetupDataGridColumns();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dealershipCarsDgr.SelectedItem != null)
                {
                    object id = (dealershipCarsDgr.SelectedItem as DataRowView).Row[0];
                    dealershipCars.DeleteQuery(Convert.ToInt32(id));
                    dealershipCarsDgr.ItemsSource = dealershipCars.GetDataBy3();
                }
                else
                {
                    MessageBox.Show("Выберите элемент для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Нельзя удалить так как связан с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            SetupDataGridColumns();
        }

        private void SetupDataGridColumns()
        {
            dealershipCarsDgr.Columns[0].Visibility = Visibility.Collapsed;
            dealershipCarsDgr.Columns[1].Visibility = Visibility.Collapsed;
            dealershipCarsDgr.Columns[2].Visibility = Visibility.Collapsed;
            dealershipCarsDgr.Columns[3].Header = "Название автосалона";
            dealershipCarsDgr.Columns[4].Header = "Машина";
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
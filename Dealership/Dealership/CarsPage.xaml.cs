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
    /// Логика взаимодействия для CarsPage.xaml
    /// </summary>
    public partial class CarsPage : Page
    {
        CarsTableAdapter cars = new CarsTableAdapter();
        BrandsTableAdapter brands = new BrandsTableAdapter();
        ModelsTableAdapter models = new ModelsTableAdapter();
        SpecificationsTableAdapter specifications = new SpecificationsTableAdapter();
        public CarsPage()
        {
            InitializeComponent();
            carsDgr.ItemsSource = cars.GetDataBy1();
            brandCbx.ItemsSource = brands.GetData();
            brandCbx.DisplayMemberPath = "BrandName";
            modelCbx.ItemsSource = models.GetData();
            modelCbx.DisplayMemberPath = "ModelName";
            specificationCbx.ItemsSource = specifications.GetData();
            specificationCbx.DisplayMemberPath = "EnginePower";
        }

        private void carsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carsDgr.SelectedItem != null)
            {
                DataRowView row = carsDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    brandCbx.Text = row.Row["BrandName"].ToString();
                    modelCbx.Text = row.Row["ModelName"].ToString();
                    specificationCbx.Text = row.Row["EnginePower"].ToString();
                    text4.Text = row.Row["YearRelease"].ToString();
                    text5.Text = row.Row["Price"].ToString();
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (brandCbx.SelectedItem != null && modelCbx.SelectedItem != null && specificationCbx.SelectedItem != null && !string.IsNullOrWhiteSpace(text4.Text) && !string.IsNullOrWhiteSpace(text5.Text))
                {
                    DataRowView selectedbrand = brandCbx.SelectedItem as DataRowView;
                    DataRowView selectedmodel = modelCbx.SelectedItem as DataRowView;
                    DataRowView selectedspecification = specificationCbx.SelectedItem as DataRowView;
                    int selectedbrandId = Convert.ToInt32(selectedbrand["ID_Brand"]);
                    int selectedmodelId = Convert.ToInt32(selectedmodel["ID_Model"]);
                    int selectedspecificationId = Convert.ToInt32(selectedspecification["ID_Specification"]);
                    cars.InsertQuery(selectedbrandId, selectedmodelId, selectedspecificationId, text4.Text, Convert.ToDecimal(text5.Text));
                    carsDgr.ItemsSource = cars.GetDataBy1();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Такой бренд и модель уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

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

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (carsDgr.SelectedItem as DataRowView)?.Row[0];
                if (id != null)
                {
                    if (brandCbx.SelectedItem != null && modelCbx.SelectedItem != null && specificationCbx.SelectedItem != null && !string.IsNullOrWhiteSpace(text4.Text) && !string.IsNullOrWhiteSpace(text5.Text))
                    {
                        DataRowView selectedbrand = brandCbx.SelectedItem as DataRowView;
                        DataRowView selectedmodel = modelCbx.SelectedItem as DataRowView;
                        DataRowView selectedspecification = specificationCbx.SelectedItem as DataRowView;
                        int selectedbrandId = Convert.ToInt32(selectedbrand["ID_Brand"]);
                        int selectedmodelId = Convert.ToInt32(selectedmodel["ID_Model"]);
                        int selectedspecificationId = Convert.ToInt32(selectedspecification["ID_Specification"]);
                        cars.UpdateQuery(selectedbrandId, selectedmodelId, selectedspecificationId, text4.Text, Convert.ToDecimal(text5.Text), Convert.ToInt32(id));
                        carsDgr.ItemsSource = cars.GetDataBy1();
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите элемент для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Такой бренд и модель уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

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

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (carsDgr.SelectedItem != null)
                {
                    object id = (carsDgr.SelectedItem as DataRowView).Row[0];
                    cars.DeleteQuery(Convert.ToInt32(id));
                    carsDgr.ItemsSource = cars.GetDataBy1();
                }
                else
                {
                    MessageBox.Show("Выберите элемент для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Нельзя удалить запись так как она связана с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

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
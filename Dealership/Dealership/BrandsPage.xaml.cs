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
using Newtonsoft.Json;
using Dealership.DealershipDataSetTableAdapters;
using System.Text.RegularExpressions;

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для BrandsPage.xaml
    /// </summary>
    public partial class BrandsPage : Page
    {
        BrandsTableAdapter brands = new BrandsTableAdapter();
        public BrandsPage()
        {
            InitializeComponent();
            brandsDgr.ItemsSource = brands.GetData();
            text1.PreviewTextInput += Text1_PreviewTextInput;
        }

        private void brandsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brandsDgr.SelectedItem != null)
            {
                DataRowView row = brandsDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["BrandName"].ToString();
                }
            }
        }

        private void Text1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(text1.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните поле перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                brands.InsertQuery(text1.Text);
                brandsDgr.ItemsSource = brands.GetData();
            }
            catch
            {
                MessageBox.Show("Такой бренд уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (brandsDgr.SelectedItem is DataRowView selectedRow)
                {
                    if (string.IsNullOrEmpty(text1.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните поле перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    object id = selectedRow.Row[0];
                    brands.UpdateQuery(text1.Text, Convert.ToInt32(id));
                    brandsDgr.ItemsSource = brands.GetData();
                }
                else
                {
                    MessageBox.Show("Нажмите на строку, которую хотите изменить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Такой бренд уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (brandsDgr.SelectedItem == null)
            {
                MessageBox.Show("Выберите бренд для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                object id = (brandsDgr.SelectedItem as DataRowView).Row[0];
                brands.DeleteQuery(Convert.ToInt32(id));
                brandsDgr.ItemsSource = brands.GetData();
            }
            catch
            {
                MessageBox.Show("Нельзя удалить бренд, так как он связан с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void UpdateColumnsVisibility()
        {
            brandsDgr.Columns[0].Visibility = Visibility.Collapsed;
            brandsDgr.Columns[1].Header = "Название бренда";
        }

        private void importData_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BrandModel> forImport1 = LabaConverter.DeserializeObject<List<BrandModel>>();

                foreach (var item in forImport1)
                {
                    brands.InsertQuery(item.BrandName);
                }

                brandsDgr.ItemsSource = null;
                brandsDgr.ItemsSource = brands.GetData();
            }
            catch { }

            brandsDgr.Columns[0].Visibility = Visibility.Collapsed;
            brandsDgr.Columns[1].Header = "Название бренда";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            brandsDgr.Columns[0].Visibility = Visibility.Collapsed;
            brandsDgr.Columns[1].Header = "Название бренда";
        }
    }
}
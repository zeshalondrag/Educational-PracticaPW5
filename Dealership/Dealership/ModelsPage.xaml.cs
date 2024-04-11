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
using Dealership.DealershipDataSetTableAdapters;
using System.Text.RegularExpressions;

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для ModelsPage.xaml
    /// </summary>
    public partial class ModelsPage : Page
    {
        ModelsTableAdapter models = new ModelsTableAdapter();

        public ModelsPage()
        {
            InitializeComponent();
            modelsDgr.ItemsSource = models.GetData();
            text1.PreviewTextInput += Text1_PreviewTextInput;
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

                models.InsertQuery(text1.Text);
                modelsDgr.ItemsSource = models.GetData();
            }
            catch
            {
                MessageBox.Show("Такая модель уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (modelsDgr.SelectedItem is DataRowView selectedRow)
                {
                    if (string.IsNullOrEmpty(text1.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните поле перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    object id = selectedRow.Row[0];
                    models.UpdateQuery(text1.Text, Convert.ToInt32(id));
                    modelsDgr.ItemsSource = models.GetData();
                }
                else
                {
                    MessageBox.Show("Нажмите на строку, которую хотите изменить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Такая модель уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (modelsDgr.SelectedItem == null)
            {
                MessageBox.Show("Выберите модель для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                object id = (modelsDgr.SelectedItem as DataRowView).Row[0];
                models.DeleteQuery(Convert.ToInt32(id));
                modelsDgr.ItemsSource = models.GetData();
            }
            catch
            {
                MessageBox.Show("Вы не можете удалить эту модель, так как она связана с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void modelsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modelsDgr.SelectedItem != null)
            {
                DataRowView row = modelsDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["ModelName"].ToString();
                }
            }
        }

        private void importData_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ModelModel> forImport = LabaConverter.DeserializeObject<List<ModelModel>>();

                foreach (var item in forImport)
                {
                    models.InsertQuery(item.ModelName);
                }

                modelsDgr.ItemsSource = null;
                modelsDgr.ItemsSource = models.GetData();
            }
            catch { }

            UpdateColumnsVisibility();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateColumnsVisibility();
        }

        private void UpdateColumnsVisibility()
        {
            modelsDgr.Columns[0].Visibility = Visibility.Collapsed;
            modelsDgr.Columns[1].Header = "ModelName";
        }
    }
}
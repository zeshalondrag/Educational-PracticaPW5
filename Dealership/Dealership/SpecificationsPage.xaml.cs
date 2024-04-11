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
    /// Логика взаимодействия для SpecificationsPage.xaml
    /// </summary>
    public partial class SpecificationsPage : Page
    {
        SpecificationsTableAdapter specifications = new SpecificationsTableAdapter();

        public SpecificationsPage()
        {
            InitializeComponent();
            specificationsDgr.ItemsSource = specifications.GetData();
        }

        private void specificationsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (specificationsDgr.SelectedItem != null)
            {
                DataRowView row = specificationsDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["EnginePower"].ToString();
                    text2.Text = row.Row["EngineCapacity"].ToString();
                    text3.Text = row.Row["MachineDrive"].ToString();
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(text1.Text) && !string.IsNullOrWhiteSpace(text2.Text) && !string.IsNullOrWhiteSpace(text3.Text))
            {
                specifications.InsertQuery(text1.Text, text2.Text, text3.Text);
                specificationsDgr.ItemsSource = specifications.GetData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля перед добавлением!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateGridColumns();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (specificationsDgr.SelectedItem is DataRowView selectedRow)
                {
                    if (!string.IsNullOrWhiteSpace(text1.Text) && !string.IsNullOrWhiteSpace(text2.Text) && !string.IsNullOrWhiteSpace(text3.Text))
                    {
                        object id = selectedRow.Row[0];
                        specifications.UpdateQuery(text1.Text, text2.Text, text3.Text, Convert.ToInt32(id));
                        specificationsDgr.ItemsSource = specifications.GetData();
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля перед редактированием!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обновлении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateGridColumns();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (specificationsDgr.SelectedItem != null)
                {
                    object id = (specificationsDgr.SelectedItem as DataRowView).Row[0];
                    specifications.DeleteQuery(Convert.ToInt32(id));
                    specificationsDgr.ItemsSource = specifications.GetData();
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Вы не можете удалить эту модель, так как она связана с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateGridColumns();
        }

        private void importData_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<SpecificationModel> forImport = LabaConverter.DeserializeObject<List<SpecificationModel>>();

                foreach (var item in forImport)
                {
                    if (!string.IsNullOrWhiteSpace(item.EnginePower) && !string.IsNullOrWhiteSpace(item.EngineCapacity) && !string.IsNullOrWhiteSpace(item.MachineDrive))
                    {
                        specifications.InsertQuery(item.EnginePower, item.EngineCapacity, item.MachineDrive);
                    }
                }

                specificationsDgr.ItemsSource = null;
                specificationsDgr.ItemsSource = specifications.GetData();
            }
            catch { }

            UpdateGridColumns();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGridColumns();
        }

        private void UpdateGridColumns()
        {
            specificationsDgr.Columns[0].Visibility = Visibility.Collapsed;
            specificationsDgr.Columns[1].Header = "Мощность двигателя";
            specificationsDgr.Columns[2].Header = "Объём двигателя";
            specificationsDgr.Columns[3].Header = "Привод";
        }
    }
}
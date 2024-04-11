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
    /// Логика взаимодействия для StatusPage.xaml
    /// </summary>
    public partial class StatusPage : Page
    {
        StatusOrdersTableAdapter status = new StatusOrdersTableAdapter();

        public StatusPage()
        {
            InitializeComponent();
            statusOrdersDgr.ItemsSource = status.GetData();
            text1.PreviewTextInput += Text1_PreviewTextInput;
        }

        private void statusOrdersDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (statusOrdersDgr.SelectedItem != null)
            {
                DataRowView row = statusOrdersDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["TypeStatus"].ToString();
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

                status.InsertQuery(text1.Text);
                statusOrdersDgr.ItemsSource = status.GetData();
            }
            catch
            {
                MessageBox.Show("Такой статус уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (statusOrdersDgr.SelectedItem is DataRowView selectedRow)
                {
                    if (string.IsNullOrEmpty(text1.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните поле перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    object id = selectedRow.Row[0];
                    status.UpdateQuery(text1.Text, Convert.ToInt32(id));
                    statusOrdersDgr.ItemsSource = status.GetData();
                }
                else
                {
                    MessageBox.Show("Выберите статус, который хотите изменить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обновлении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (statusOrdersDgr.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                object id = (statusOrdersDgr.SelectedItem as DataRowView).Row[0];
                status.DeleteQuery(Convert.ToInt32(id));
                statusOrdersDgr.ItemsSource = status.GetData();
            }
            catch
            {
                MessageBox.Show("Вы не можете удалить этот статус, так как он связан с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateColumnsVisibility();
        }

        private void UpdateColumnsVisibility()
        {
            statusOrdersDgr.Columns[0].Visibility = Visibility.Collapsed;
            statusOrdersDgr.Columns[1].Header = "Тип статуса";
        }
    }
}

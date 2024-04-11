using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для DealershipPage.xaml
    /// </summary>
    public partial class DealershipPage : Page
    {
        DealershipTableAdapter dealership = new DealershipTableAdapter();

        public DealershipPage()
        {
            InitializeComponent();
            dealershipDgr.ItemsSource = dealership.GetData();
            text1.PreviewTextInput += Text1_PreviewTextInput;
            text2.PreviewTextInput += Text2_PreviewTextInput;
        }

        private void Text1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем ввод только букв
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Text2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dealership.InsertQuery(text1.Text, text2.Text);
                dealershipDgr.ItemsSource = dealership.GetData();
            }
            catch
            {
                MessageBox.Show("Название и адрес с такими данными уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dealershipDgr.SelectedItem is DataRowView selectedRow)
                {
                    if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    object id = selectedRow.Row[0];
                    dealership.UpdateQuery(text1.Text, text2.Text, Convert.ToInt32(id));
                    dealershipDgr.ItemsSource = dealership.GetData();
                }
                else
                {
                    MessageBox.Show("Выберите строку для изменения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            if (dealershipDgr.SelectedItem == null)
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                object id = (dealershipDgr.SelectedItem as DataRowView).Row[0];
                dealership.DeleteQuery(Convert.ToInt32(id));
                dealershipDgr.ItemsSource = dealership.GetData();
            }
            catch
            {
                MessageBox.Show("Нельзя удалить, так как есть связь с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void dealershipDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dealershipDgr.SelectedItem != null)
            {
                DataRowView row = dealershipDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["DealershipName"].ToString();
                    text2.Text = row.Row["Adress"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateColumnsVisibility();
        }

        private void UpdateColumnsVisibility()
        {
            dealershipDgr.Columns[0].Visibility = Visibility.Collapsed;
            dealershipDgr.Columns[1].Header = "Название автосалона";
            dealershipDgr.Columns[2].Header = "Адресс";
        }
    }
}
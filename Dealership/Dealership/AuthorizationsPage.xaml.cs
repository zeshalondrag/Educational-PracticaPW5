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
using System.Text.RegularExpressions;
using Dealership.DealershipDataSetTableAdapters;


namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationsPage.xaml
    /// </summary>
    public partial class AuthorizationsPage : Page
    {
        AuthorizationsTableAdapter auth = new AuthorizationsTableAdapter();
        public AuthorizationsPage()
        {
            InitializeComponent();
            authDgr.ItemsSource = auth.GetData();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (text1.Text.Length < 5 || text1.Text.Length > 15 || !IsValidInput(text1.Text) || !IsValidInput(text2.Password))
                {
                    MessageBox.Show("Логин и пароль должны содержать от 5 до 15 букв и цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                auth.InsertQuery(text1.Text, text2.Password);
                authDgr.ItemsSource = auth.GetData();
            }
            catch
            {
                MessageBox.Show("Такой логин уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (text1.Text.Length < 5 || text1.Text.Length > 15 || !IsValidInput(text1.Text) || !IsValidInput(text2.Password))
                {
                    MessageBox.Show("Логин и пароль должны содержать от 5 до 15 букв и цифр!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (authDgr.SelectedItem != null && authDgr.SelectedItem is DataRowView selectedRow)
                {
                    object id = selectedRow.Row[0];
                    auth.UpdateQuery(text1.Text, text2.Password, Convert.ToInt32(id));
                    authDgr.ItemsSource = auth.GetData();
                }
                else
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Такой логин уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (authDgr.SelectedItem == null)
                {
                    MessageBox.Show("Выберите запись для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                object id = (authDgr.SelectedItem as DataRowView).Row[0];
                auth.DeleteQuery(Convert.ToInt32(id));
                authDgr.ItemsSource = auth.GetData();
            }
            catch 
            {
                MessageBox.Show("Логин связан с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private bool IsValidInput(string input)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(input);
        }

        private void UpdateColumnsVisibility()
        {
            authDgr.Columns[0].Visibility = Visibility.Collapsed;
            authDgr.Columns[1].Header = "Логин";
            authDgr.Columns[2].Visibility = Visibility.Collapsed;
        }


        private void authDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (authDgr.SelectedItem != null)
            {
                DataRowView row = authDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["LoginA"].ToString();
                    text2.Password = row.Row["PasswordA"].ToString();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            authDgr.Columns[0].Visibility = Visibility.Collapsed;
            authDgr.Columns[1].Header = "Логин";
            authDgr.Columns[2].Visibility = Visibility.Collapsed;
        }
    }
}
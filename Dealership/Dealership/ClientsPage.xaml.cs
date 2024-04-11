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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        ClientsTableAdapter clients = new ClientsTableAdapter();
        AuthorizationsTableAdapter authorizations = new AuthorizationsTableAdapter();
        public ClientsPage()
        {
            InitializeComponent();
            clientsDgr.ItemsSource = clients.GetFullInfo();
            loginCbx.ItemsSource = authorizations.GetData();
            loginCbx.DisplayMemberPath = "LoginA";
            text4.PreviewTextInput += PhoneTextBox_PreviewTextInput;
            text4.TextChanged += PhoneTextBox_TextChanged;
            text4.GotFocus += Text4_GotFocus;

            text1.PreviewTextInput += Text_PreviewTextInput;
            text2.PreviewTextInput += Text_PreviewTextInput;
            text3.PreviewTextInput += Text_PreviewTextInput;
        }

        private void clientsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clientsDgr.SelectedItem != null)
            {
                DataRowView row = clientsDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["ClientSurname"].ToString();
                    text2.Text = row.Row["ClientName"].ToString();
                    text3.Text = row.Row["ClientMiddlename"].ToString();
                    text4.Text = row.Row["PhoneNumber"].ToString();
                    loginCbx.Text = row.Row["LoginA"].ToString();
                }
            }
        }

        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }


        private void Text4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(text4.Text) || !text4.Text.StartsWith("+7"))
            {
                text4.Text = "+7";
                text4.CaretIndex = text4.Text.Length; 
            }
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (text4.Text.Length > 12)
            {
                text4.Text = text4.Text.Substring(0, 12);
                text4.CaretIndex = text4.Text.Length;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Text) || string.IsNullOrEmpty(text3.Text) || string.IsNullOrEmpty(text4.Text) || loginCbx.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (loginCbx.SelectedItem is DataRowView selectedauth)
                {
                    int selectedauthId = Convert.ToInt32(selectedauth["ID_Authorizations"]);

                    if (text4.Text.Length < 12)
                    {
                        MessageBox.Show("Номер телефона должен содержать минимум 12 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    clients.InsertQuery(text1.Text, text2.Text, text3.Text, text4.Text, selectedauthId);
                    clientsDgr.ItemsSource = clients.GetFullInfo();
                }
            }
            catch
            {
                MessageBox.Show("Этот логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clientsDgr.SelectedItem != null && clientsDgr.SelectedItem is DataRowView selectedRow)
                {
                    if (loginCbx.SelectedItem is DataRowView selectedauth)
                    {
                        int selectedauthId = Convert.ToInt32(selectedauth["ID_Authorizations"]);
                        object id = selectedRow.Row[0];

                        if (text4.Text.Length < 12)
                        {
                            MessageBox.Show("Номер телефона должен содержать минимум 12 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        try
                        {
                            clients.UpdateQuery(text1.Text, text2.Text, text3.Text, text4.Text, selectedauthId, Convert.ToInt32(id));
                            clientsDgr.ItemsSource = clients.GetFullInfo();
                        }
                        catch 
                        {
                            MessageBox.Show("Этот логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Нажмите на строку которую хотите изменить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            UpdateColumnsVisibility();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clientsDgr.SelectedItem == null)
                {
                    MessageBox.Show("Выберите клиента для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                object id = (clientsDgr.SelectedItem as DataRowView).Row[0];
                clients.DeleteQuery(Convert.ToInt32(id));
                clientsDgr.ItemsSource = clients.GetFullInfo();
            }
            catch 
            {
                MessageBox.Show("Этот логин связан с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void UpdateColumnsVisibility()
        {
            clientsDgr.Columns[0].Visibility = Visibility.Collapsed;
            clientsDgr.Columns[5].Visibility = Visibility.Collapsed;
            clientsDgr.Columns[1].Header = "Фамилия";
            clientsDgr.Columns[2].Header = "Имя";
            clientsDgr.Columns[3].Header = "Отчество";
            clientsDgr.Columns[4].Header = "Номер телефона";
            clientsDgr.Columns[6].Header = "Логин";
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            clientsDgr.Columns[0].Visibility = Visibility.Collapsed;
            clientsDgr.Columns[5].Visibility = Visibility.Collapsed;
            clientsDgr.Columns[1].Header = "Фамилия";
            clientsDgr.Columns[2].Header = "Имя";
            clientsDgr.Columns[3].Header = "Отчество";
            clientsDgr.Columns[4].Header = "Номер телефона";
            clientsDgr.Columns[6].Header = "Логин";
        }
    }
}
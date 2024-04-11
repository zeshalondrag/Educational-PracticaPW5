using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dealership.DealershipDataSetTableAdapters;

namespace Dealership
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        PositionsTableAdapter pos = new PositionsTableAdapter();
        AuthorizationsTableAdapter auth = new AuthorizationsTableAdapter();

        public EmployeesPage()
        {
            InitializeComponent();
            employeesDgr.ItemsSource = employees.GetDataBy1();
            positionCbx.ItemsSource = pos.GetData();
            positionCbx.DisplayMemberPath = "NamePosition";
            loginCbx.ItemsSource = auth.GetData();
            loginCbx.DisplayMemberPath = "LoginA";

            text4.PreviewTextInput += PhoneTextBox_PreviewTextInput;
            text4.TextChanged += PhoneTextBox_TextChanged;
            text4.GotFocus += Text4_GotFocus;

            text1.PreviewTextInput += Text_PreviewTextInput;
            text2.PreviewTextInput += Text_PreviewTextInput;
            text3.PreviewTextInput += Text_PreviewTextInput;
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
                if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Text) || string.IsNullOrEmpty(text3.Text) || string.IsNullOrEmpty(text4.Text) || loginCbx.SelectedItem == null || positionCbx.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (loginCbx.SelectedItem is DataRowView selectedauth && positionCbx.SelectedItem is DataRowView selectedpos)
                {
                    int selectedposId = Convert.ToInt32(selectedpos["ID_Position"]);
                    int selectedauthId = Convert.ToInt32(selectedauth["ID_Authorizations"]);

                    if (text4.Text.Length < 12)
                    {
                        MessageBox.Show("Номер телефона должен содержать минимум 12 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    employees.InsertQuery(text1.Text, text2.Text, text3.Text, text4.Text, selectedposId, selectedauthId);
                    employeesDgr.ItemsSource = employees.GetDataBy1();
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
                if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Text) || string.IsNullOrEmpty(text3.Text) || string.IsNullOrEmpty(text4.Text) || loginCbx.SelectedItem == null || positionCbx.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (loginCbx.SelectedItem is DataRowView selectedauth && positionCbx.SelectedItem is DataRowView selectedpos)
                {
                    int selectedposId = Convert.ToInt32(selectedpos["ID_Position"]);
                    int selectedauthId = Convert.ToInt32(selectedauth["ID_Authorizations"]);
                    object id = (employeesDgr.SelectedItem as DataRowView)?.Row[0];

                    if (id != null)
                    {
                        if (text4.Text.Length < 12)
                        {
                            MessageBox.Show("Номер телефона должен содержать минимум 12 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        employees.UpdateQuery(text1.Text, text2.Text, text3.Text, text4.Text, selectedposId, selectedauthId, Convert.ToInt32(id));
                        employeesDgr.ItemsSource = employees.GetDataBy1();
                    }
                    else
                    {
                        MessageBox.Show("Выберите сотрудника, которого хотите изменить!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите должность и авторизацию для сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Этот логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (employeesDgr.SelectedItem == null)
                {
                    MessageBox.Show("Выберите сотрудника для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                object id = (employeesDgr.SelectedItem as DataRowView).Row[0];
                employees.DeleteQuery(Convert.ToInt32(id));
                employeesDgr.ItemsSource = employees.GetDataBy1();
            }
            catch
            {
                MessageBox.Show("Этот логин связан с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }


        private void employeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeesDgr.SelectedItem != null)
            {
                DataRowView row = employeesDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    positionCbx.Text = row.Row["NamePosition"].ToString();
                    loginCbx.Text = row.Row["LoginA"].ToString();
                    text1.Text = row.Row["EmployeeSurname"].ToString();
                    text2.Text = row.Row["EmployeeName"].ToString();
                    text3.Text = row.Row["EmployeeMiddlename"].ToString();
                    text4.Text = row.Row["PhoneNumber"].ToString();
                }
            }
        }

        private void UpdateColumnsVisibility()
        {
            employeesDgr.Columns[0].Visibility = Visibility.Collapsed;
            employeesDgr.Columns[5].Visibility = Visibility.Collapsed;
            employeesDgr.Columns[6].Visibility = Visibility.Collapsed;
            employeesDgr.Columns[1].Header = "Фамилия";
            employeesDgr.Columns[2].Header = "Имя";
            employeesDgr.Columns[3].Header = "Отчество";
            employeesDgr.Columns[4].Header = "Номер телефона";
            employeesDgr.Columns[7].Header = "Название должности";
            employeesDgr.Columns[8].Header = "Логин";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateColumnsVisibility();
        }
    }
}
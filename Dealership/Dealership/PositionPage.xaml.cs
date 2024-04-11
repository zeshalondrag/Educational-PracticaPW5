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
    /// Логика взаимодействия для PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Page
    {
        PositionsTableAdapter positions = new PositionsTableAdapter();

        public PositionPage()
        {
            InitializeComponent();
            positionsDgr.ItemsSource = positions.GetData();
            text2.PreviewTextInput += Text2_PreviewTextInput;
            text3.PreviewTextInput += Text3_PreviewTextInput;
            text1.PreviewTextInput += Text1_PreviewTextInput;
        }

        private void positionsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (positionsDgr.SelectedItem != null)
            {
                DataRowView row = positionsDgr.SelectedItem as DataRowView;
                if (row != null)
                {
                    text1.Text = row.Row["NamePosition"].ToString();
                    text2.Text = row.Row["WorkSchedule"].ToString();
                    text3.Text = row.Row["Wages"].ToString();
                }
            }
        }

        private void Text1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void Text2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length >= 3)
            {
                e.Handled = true;
                return;
            }

            Regex regex = new Regex("[^0-9/]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void Text3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Яa-zA-Z]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Text) || string.IsNullOrEmpty(text3.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля перед добавлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                positions.InsertQuery(text1.Text, text2.Text, Convert.ToDecimal(text3.Text));
                positionsDgr.ItemsSource = positions.GetData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Заработная плата должна быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch 
            {
                MessageBox.Show("Такая должность уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (positionsDgr.SelectedItem is DataRowView selectedRow)
                {
                    if (string.IsNullOrEmpty(text1.Text) || string.IsNullOrEmpty(text2.Text) || string.IsNullOrEmpty(text3.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля перед изменением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    object id = selectedRow.Row[0];
                    positions.UpdateQuery(text1.Text, text2.Text, Convert.ToDecimal(text3.Text), Convert.ToInt32(id));
                    positionsDgr.ItemsSource = positions.GetData();
                }
                else
                {
                    MessageBox.Show("Выберите должность для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Заработная плата должна быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Такая должность уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (positionsDgr.SelectedItem == null)
            {
                MessageBox.Show("Выберите должность для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                object id = (positionsDgr.SelectedItem as DataRowView).Row[0];
                positions.DeleteQuery(Convert.ToInt32(id));
                positionsDgr.ItemsSource = positions.GetData();
            }
            catch
            {
                MessageBox.Show("Эта должность связана с другой таблицей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateColumnsVisibility();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            positionsDgr.Columns[0].Visibility = Visibility.Collapsed;
            positionsDgr.Columns[1].Header = "Название должности";
            positionsDgr.Columns[2].Header = "График работы";
            positionsDgr.Columns[3].Header = "Заработная плата";
        }

        private void UpdateColumnsVisibility()
        {
            positionsDgr.Columns[0].Visibility = Visibility.Collapsed;
            positionsDgr.Columns[1].Header = "Название должности";
            positionsDgr.Columns[2].Header = "График работы";
            positionsDgr.Columns[3].Header = "Заработная плата";
        }
    }
}
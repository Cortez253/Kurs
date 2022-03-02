using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для ServicesWindow.xaml
    /// </summary>
    public partial class ServicesWindow : Window
    {
        Entities entities = new Entities();
        public ServicesWindow()
        {
            InitializeComponent();
            foreach (var ServiceName in entities.Services)
                ListBoxServices.Items.Add(ServiceName);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var addService = ListBoxServices.SelectedItem as Services;

            if (TextBoxName.Text == "" || TextBoxPrice.Text == "" )
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (addService == null)
                {
                    addService = new Services();
                    entities.Services.Add(addService);
                    ListBoxServices.Items.Add(addService);
                }
                addService.Name_service = TextBoxName.Text;
                addService.Price = decimal.Parse(TextBoxPrice.Text);

                entities.SaveChanges();
                ListBoxServices.Items.Refresh();

                MessageBox.Show("Запись сохранена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var delService = ListBoxServices.SelectedItem as Services;

            try
            {
                var ex = (from order in entities.Orders where order.Id_service == delService.Id_service select order).First();
                MessageBox.Show("Невозможно удалить выбранную услугу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {

                if (delService != null)
                {
                    var res = MessageBox.Show("Удалить выбранную запись?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                    entities.Services.Remove(delService);
                    ListBoxServices.Items.Remove(delService);
                    entities.SaveChanges();
                    ListBoxServices.Items.Refresh();
                    TextBoxName.Clear();
                    TextBoxPrice.Clear();
                    MessageBox.Show("Запись удалена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Запись не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ListBoxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectService = ListBoxServices.SelectedItem as Services;

            if (selectService != null)
            {
                TextBoxName.Text = selectService.Name_service;
                TextBoxPrice.Text = selectService.Price.ToString();
            }
            else
            {
                TextBoxName.Text = "";
                TextBoxPrice.Text = "";
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow order = new OrderWindow();
            Close();
            order.ShowDialog();
        }


        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            ListBoxServices.Items.Clear();
            ListBoxServices.Items.Refresh();
            if (string.IsNullOrWhiteSpace(TextBoxSearch.Text))
            {
                foreach (var item in entities.Services)
                {
                    ListBoxServices.Items.Add(item);
                    ListBoxServices.Items.Refresh();
                }
            }
            else
            {
                foreach (var service in entities.Services)
                {
                    if (service.Name_service.StartsWith(TextBoxSearch.Text))
                    {
                        ListBoxServices.Items.Add(service);
                    }
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Text = "";
            TextBoxPrice.Text = "";
            ListBoxServices.SelectedIndex = -1;
        }
    }
}

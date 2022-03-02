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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        Entities entities = new Entities();
        public AddOrderWindow()
        {
            InitializeComponent();

            foreach (var client in entities.Clients)
                ClientComboBox.Items.Add(client);
            foreach (var avto in entities.Avto)
                AvtoComboBox.Items.Add(avto);
            foreach (var service in entities.Services)
                ServiceComboBox.Items.Add(service);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ClientComboBox.SelectedIndex == -1 || ServiceComboBox.SelectedIndex == -1 || AvtoComboBox.SelectedIndex == -1 || DatePickerOrder.SelectedDate == null || TextBoxPeriod.Text == "")
            {
                MessageBox.Show("Для добавления заказа необходимо заполнить все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Orders orders = new Orders();
                entities.Orders.Add(orders);
                orders.Id_client = (ClientComboBox.SelectedItem as Clients).Id_client;
                orders.Id_avto = (AvtoComboBox.SelectedItem as Avto).Id_avto;
                orders.Id_service = (ServiceComboBox.SelectedItem as Services).Id_service;
                orders.Date_order = (DateTime)DatePickerOrder.SelectedDate;
                orders.Period_of_execution = TextBoxPeriod.Text;
                if (RadioButtonProcess.IsChecked == true)
                {
                    orders.Order_status = "Выполняется";
                }
                else
                {
                    orders.Order_status = "Завершен";
                }
                entities.SaveChanges();

                MessageBox.Show("Информация добавлена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow2 order = new OrderWindow2();
            Close();
            order.ShowDialog();
        }
    }
}

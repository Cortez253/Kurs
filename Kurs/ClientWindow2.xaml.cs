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
    /// Логика взаимодействия для ClientWindow2.xaml
    /// </summary>
    public partial class ClientWindow2 : Window
    {
        Entities entities = new Entities();
        public ClientWindow2()
        {
            InitializeComponent();
            foreach (var client in entities.Clients)
                ListBoxClients.Items.Add(client);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var addClient = ListBoxClients.SelectedItem as Clients;

            if (TextBoxSurname.Text == "" || TextBoxName.Text == "" || TextBoxTelephone.Text == "")
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (addClient == null)
                {
                    addClient = new Clients();
                    entities.Clients.Add(addClient);
                    ListBoxClients.Items.Add(addClient);
                }
                addClient.Surname = TextBoxSurname.Text;
                addClient.Name = TextBoxName.Text;
                addClient.Telephone = TextBoxTelephone.Text;
                addClient.E_mail = TextBoxEmail.Text;

                entities.SaveChanges();
                ListBoxClients.Items.Refresh();

                MessageBox.Show("Запись сохранена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var delClient = ListBoxClients.SelectedItem as Clients;

            try
            {
                var ex = (from order in entities.Orders where order.Id_client == delClient.Id_client select order).First();
                MessageBox.Show("Невозможно удалить выбранного заказчика", "Оишбка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {

                if (delClient != null)
                {
                    var res = MessageBox.Show("Удалить выбранную запись?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                    entities.Clients.Remove(delClient);
                    ListBoxClients.Items.Remove(delClient);
                    entities.SaveChanges();
                    ListBoxClients.Items.Refresh();
                    TextBoxName.Clear();
                    TextBoxSurname.Clear();
                    TextBoxTelephone.Clear();
                    TextBoxEmail.Clear();
                    MessageBox.Show("Запись удалена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Запись не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSurname.Text = "";
            TextBoxName.Text = "";
            TextBoxTelephone.Text = "";
            TextBoxEmail.Text = "";
            ListBoxClients.SelectedIndex = -1;
        }

        private void ListBoxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectClient = ListBoxClients.SelectedItem as Clients;

            if (selectClient != null)
            {
                TextBoxName.Text = selectClient.Name;
                TextBoxSurname.Text = selectClient.Surname;
                TextBoxTelephone.Text = selectClient.Telephone;
                TextBoxEmail.Text = selectClient.E_mail;
            }
            else
            {
                TextBoxName.Text = "";
                TextBoxSurname.Text = "";
                TextBoxTelephone.Text = "";
                TextBoxEmail.Text = "";
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow2 orderWin = new OrderWindow2();
            Close();
            orderWin.ShowDialog();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            ListBoxClients.Items.Clear();
            ListBoxClients.Items.Refresh();
            if (string.IsNullOrWhiteSpace(TextBoxSearch.Text))
            {
                foreach (var item in entities.Clients)
                {
                    ListBoxClients.Items.Add(item);
                    ListBoxClients.Items.Refresh();
                }
            }
            else
            {
                foreach (var a in entities.Clients)
                {
                    if (a.Surname.StartsWith(TextBoxSearch.Text))
                    {
                        ListBoxClients.Items.Add(a);
                    }
                }
            }
        }
    }
}

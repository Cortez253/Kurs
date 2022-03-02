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
    /// Логика взаимодействия для AvtoWindow.xaml
    /// </summary>
    public partial class AvtoWindow : Window
    {
        Entities entities = new Entities();
        public AvtoWindow()
        {
            InitializeComponent();
            foreach (var avto in entities.Avto)
                ListBoxAvto.Items.Add(avto);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var addAvto = ListBoxAvto.SelectedItem as Avto;

            if (TextBoxBrand.Text == "" || TextBoxModel.Text == "" )
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (addAvto == null)
                {
                    addAvto = new Avto();
                    entities.Avto.Add(addAvto);
                    ListBoxAvto.Items.Add(addAvto);
                }
                addAvto.Car_brand = TextBoxBrand.Text;
                addAvto.Model = TextBoxModel.Text;

                entities.SaveChanges();
                ListBoxAvto.Items.Refresh();

                MessageBox.Show("Запись сохранена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var delAvto = ListBoxAvto.SelectedItem as Avto;

            try
            {
                var ex = (from order in entities.Orders where order.Id_avto == delAvto.Id_avto select order).First();
                MessageBox.Show("Невозможно удалить выбранный автомобиль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {

                if (delAvto != null)
                {
                    var res = MessageBox.Show("Удалить выбранную запись?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                    entities.Avto.Remove(delAvto);
                    ListBoxAvto.Items.Remove(delAvto);
                    entities.SaveChanges();
                    ListBoxAvto.Items.Refresh();
                    TextBoxBrand.Clear();
                    TextBoxModel.Clear();
                    MessageBox.Show("Запись удалена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Запись не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxBrand.Text = "";
            TextBoxModel.Text = "";
            ListBoxAvto.SelectedIndex = -1;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow order = new OrderWindow();
            Close();
            order.ShowDialog();
        }

        private void ListBoxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectAvto = ListBoxAvto.SelectedItem as Avto;

            if (selectAvto != null)
            {
                TextBoxBrand.Text = selectAvto.Car_brand;
                TextBoxModel.Text = selectAvto.Model;
            }
            else
            {
                TextBoxBrand.Text = "";
                TextBoxModel.Text = "";
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            ListBoxAvto.Items.Clear();
            ListBoxAvto.Items.Refresh();
            if (string.IsNullOrWhiteSpace(TextBoxSearch.Text))
            {
                foreach (var item in entities.Avto)
                {
                    ListBoxAvto.Items.Add(item);
                    ListBoxAvto.Items.Refresh();
                }
            }
            else
            {
                foreach (var a in entities.Avto)
                {
                    if (a.Car_brand.StartsWith(TextBoxSearch.Text))
                    {
                        ListBoxAvto.Items.Add(a);

                    }
                }
            }

        }
    }
}

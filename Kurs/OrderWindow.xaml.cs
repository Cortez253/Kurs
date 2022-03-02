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
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;



namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        Entities entities = new Entities();
        public OrderWindow()
        {
            InitializeComponent();
            foreach (var client in entities.Clients)
                ClientComboBox.Items.Add(client);
            foreach (var avto in entities.Avto)
                AvtoComboBox.Items.Add(avto);
            foreach (var service in entities.Services)
                ServiceComboBox.Items.Add(service);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from order in entities.Orders select order;
            OrderGrid.ItemsSource = query.ToList();
            //var query =
            //    from order in entities.Orders
            //    select new { order.Id_order, order.Clients.Surname, order.Clients.Name, order.Avto.Car_brand, order.Avto.Model, order.Services.Name_service, order.Date_order, order.Period_of_execution, order.Order_status };

        }

        private void ClientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow client_window = new ClientWindow();
            Close();
            client_window.ShowDialog();
        }

        private void AvtoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AvtoWindow avto_window = new AvtoWindow();
            Close();
            avto_window.ShowDialog();
        }

        private void ServiceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ServicesWindow service_window = new ServicesWindow();
            Close();
            service_window.ShowDialog();
        }


        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            //int id = (OrderGrid.SelectedItem as Orders).Id_order;
            //Orders order = (from i in entities.Orders where i.Id_order == id select i).SingleOrDefault();
            //entities.Orders.Remove(order);
            //entities.SaveChanges();
            //OrderGrid.ItemsSource = entities.Orders.ToList();

            var rez = MessageBox.Show("Удалить выбранную запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rez == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                var person = OrderGrid.SelectedItem as Orders;
                if (person == null)
                    return;
                Orders order = (from i in entities.Orders where i.Id_order == person.Id_order select i).SingleOrDefault();
                entities.Orders.Remove(order);
                entities.SaveChanges();
                OrderGrid.ItemsSource = entities.Orders.ToList();

                ClientComboBox.SelectedIndex = -1;
                AvtoComboBox.SelectedIndex = -1;
                ServiceComboBox.SelectedIndex = -1;
                DatePickerOrder.SelectedDate = null;
                TextBoxPeriod.Text = "";
                RadioButtonProcess.IsChecked = false;
                RadioButtonCompleted.IsChecked = false;
            }
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.ShowDialog();
            
        }


        private void AddExcel_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();

            DataTable order = new DataTable("Orders");
            order.Columns.Add("Клиент");
            order.Columns.Add("Авто");
            order.Columns.Add("Услуга");
            order.Columns.Add("Дата заказа");
            order.Columns.Add("Время выполения");
            order.Columns.Add("Статуст заказа");
            foreach (var orders in entities.Orders)
                order.Rows.Add(orders.Clients, orders.Avto, orders.Services, orders.Date_order, orders.Period_of_execution, orders.Order_status);

            DataSet dataset = new DataSet("Orders");
            dataset.Tables.Add(order);

            orderWindow.ExportDataSetToExcel(dataset);
        }


        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortingComboBox.SelectedItem == Сompleted_order)
            {
                var query =
                    from order in entities.Orders
                    where order.Order_status == "Выполняется"
                    select order;
                    
                OrderGrid.ItemsSource = query.ToList();
            }
            else if (SortingComboBox.SelectedItem == Finish_order)
            {
                var query =
                    from order in entities.Orders
                    where order.Order_status == "Завершен"
                    select order; 
                OrderGrid.ItemsSource = query.ToList();
            }
            else if (SortingComboBox.SelectedItem == Engine_repair)
            {
                
                var query =
                    from order in entities.Orders
                    where order.Id_service == 1
                    select order;
                OrderGrid.ItemsSource = query.ToList();
            }
            else if (SortingComboBox.SelectedItem == Oil_change)
            {

                var query =
                    from order in entities.Orders
                    where order.Id_service == 2
                    select order;
                OrderGrid.ItemsSource = query.ToList();
            }
            else if (SortingComboBox.SelectedItem == Diagnostics)
            {

                var query =
                    from order in entities.Orders
                    where order.Id_service == 3
                    select order;
                OrderGrid.ItemsSource = query.ToList();
            }
            else if (SortingComboBox.SelectedItem == Replacement_of_liquids)
            {

                var query =
                    from order in entities.Orders
                    where order.Id_service == 4
                    select order;
                OrderGrid.ItemsSource = query.ToList();
            }
            else if (SortingComboBox.SelectedItem == Autoelectrics_repair)
            {

                var query =
                    from order in entities.Orders
                    where order.Id_service == 5
                    select order;
                OrderGrid.ItemsSource = query.ToList();
            }
            else if (SortingComboBox.SelectedItem == Сonditioners_repair)
            {

                var query =
                    from order in entities.Orders
                    where order.Id_service == 6
                    select order;
                OrderGrid.ItemsSource = query.ToList();
            }
            else
            {
                var query =
                    from order in entities.Orders
                    select order;
                OrderGrid.ItemsSource = query.ToList();
            }
        }

        private void ExportDataSetToExcel(DataSet ds)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);

            foreach (DataTable table in ds.Tables)
            {
                Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
        }

        private void TextBoxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            SortingComboBox.SelectedIndex = -1;
            if(txt != null)
            {
                string serch = txt.Text;
                if(!string.IsNullOrEmpty(serch))
                {
                    var query =
                        from order in entities.Orders
                        where order.Clients.Surname.Contains(serch)
                        select order;
                    OrderGrid.ItemsSource = query.ToList();
                }
                else
                {
                    var query = from order in entities.Orders select order;
                    OrderGrid.ItemsSource = query.ToList();
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var selectOrder = OrderGrid.SelectedItem as Orders;

            if (ClientComboBox.SelectedIndex == -1 || AvtoComboBox.SelectedIndex == -1 || ServiceComboBox.SelectedIndex == -1 || DatePickerOrder.SelectedDate == null || TextBoxPeriod.Text == "" || RadioButtonProcess.IsChecked == false && RadioButtonCompleted.IsChecked == false )
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (selectOrder == null)
                {
                    selectOrder = new Orders();
                    entities.Orders.Add(selectOrder);
                    
                }

                selectOrder.Id_client = (ClientComboBox.SelectedItem as Clients).Id_client;
                selectOrder.Id_avto = (AvtoComboBox.SelectedItem as Avto).Id_avto;
                selectOrder.Id_service = (ServiceComboBox.SelectedItem as Services).Id_service;
                selectOrder.Date_order = (DateTime)DatePickerOrder.SelectedDate;
                selectOrder.Period_of_execution = TextBoxPeriod.Text;
                if (RadioButtonProcess.IsChecked == true)
                {
                    selectOrder.Order_status = "Выполняется";
                }
                else
                {
                    selectOrder.Order_status = "Завершен";
                }

                entities.SaveChanges();
                OrderGrid.ItemsSource = entities.Orders.ToList();

                MessageBox.Show("Запись сохранена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OrderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectOrder = OrderGrid.SelectedItem as Orders;

            if (selectOrder != null)
            {
                ClientComboBox.SelectedItem = (from client in entities.Clients where selectOrder.Id_client == client.Id_client select client).Single<Clients>();
                AvtoComboBox.SelectedItem = (from car in entities.Avto where selectOrder.Id_avto == car.Id_avto select car).Single<Avto>();
                ServiceComboBox.SelectedItem = (from service in entities.Services where selectOrder.Id_service == service.Id_service select service).Single<Services>();
                TextBoxPeriod.Text = selectOrder.Period_of_execution;
                DatePickerOrder.SelectedDate = selectOrder.Date_order;
                if (selectOrder.Order_status == "Выполняется")
                {
                    RadioButtonProcess.IsChecked = true;
                }
                else
                {
                    RadioButtonCompleted.IsChecked = true;
                }
            }

        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ClientComboBox.SelectedIndex = -1;
            AvtoComboBox.SelectedIndex = -1;
            ServiceComboBox.SelectedIndex = -1;
            DatePickerOrder.SelectedDate = null;
            TextBoxPeriod.Text = "";
            RadioButtonProcess.IsChecked = false;
            RadioButtonCompleted.IsChecked = false;
            OrderGrid.SelectedIndex = -1;
        }

    }
}

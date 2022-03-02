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
    /// Логика взаимодействия для OrderWindow2.xaml
    /// </summary>
    public partial class OrderWindow2 : Window
    {
        Entities entities = new Entities();
        public OrderWindow2()
        {
            InitializeComponent();
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

        private void AddOrderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow order_window = new AddOrderWindow();
            Close();
            order_window.ShowDialog();
        }

        private void ClientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow2 client_window = new ClientWindow2();
            Close();
            client_window.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from order in entities.Orders select order;
            OrderGrid.ItemsSource = query.ToList();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.ShowDialog();
        }


        private void AddExcel_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow2 orderWindow = new OrderWindow2();

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
            if (txt != null)
            {
                string serch = txt.Text;
                if (!string.IsNullOrEmpty(serch))
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
    }
}

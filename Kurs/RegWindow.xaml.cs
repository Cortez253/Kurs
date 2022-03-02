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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        Entities entities = new Entities();
        public RegWindow()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text.Length <= 3 || PasswordBox.Password != PasswordBox_Repeat.Password || PasswordBox.Password.Length <= 3)
            {
                MessageBox.Show("Данные введены не корректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Users user = new Users();
                user.Login = LoginTextBox.Text;
                user.Password = PasswordBox.Password;
                entities.Users.Add(user);
                entities.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            Close();
            win.ShowDialog();
        }
    }
}

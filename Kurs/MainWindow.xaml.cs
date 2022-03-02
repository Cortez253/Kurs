using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Kurs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        public MainWindow()
        {
            InitializeComponent();
            
                

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            bool rez = false;
            if (LoginTextBox.Text == "")
            {
                MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (PasswordBox.Password == "")
            {
                MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (var user in entities.Users)
                {

                    if (user.Login == LoginTextBox.Text && user.Password == PasswordBox.Password)
                    {
                        if (user.Access_rights == "admin")
                        {
                            OrderWindow OrdWin = new OrderWindow();
                            Close();
                            OrdWin.ShowDialog();
                            rez = true;
                        }
                        else
                        {
                            OrderWindow2 OrdWin = new OrderWindow2();
                            Close();
                            OrdWin.ShowDialog();
                            rez = true;
                        }

                    }

                }
                if (rez == false)
                {
                    MessageBox.Show("Указан не верный логин или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

    


        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegWindow reg_window = new RegWindow();
            Close();
            reg_window.ShowDialog();
        }
    }
}


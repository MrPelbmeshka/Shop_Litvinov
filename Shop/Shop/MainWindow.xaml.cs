﻿using Microsoft.VisualBasic.ApplicationServices;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            var login = loginAuthorization.Text;
            var password = passwordAuthorization.Password;
            var context = new AppDbContext();
            var user = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);
            if (user is null)
            {
                MessageBox.Show("Неправильный логин или пароль!");
                return;
            }
            MessageBox.Show("Вы успешно вошли в аккаунт!");

            int b = user.Balance;
            bool b2 = false;


            Main gameWindow = new Main(b, b2);
            gameWindow.Show();
            this.Close();
        }
        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            var login = loginSign.Text;
            var pass = passwordSign.Password;
            var context = new AppDbContext();
            var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exists is not null)
            {
                MessageBox.Show("Такой пользователь уже в клубе крутышек"); 
                return;
            }

            var user = new User { Login = login, Password = pass, Balance = 5000 };
            context.Users.Add(user);
            context.SaveChanges();
            MessageBox.Show("ты зашёл");

            int b = user.Balance;
            bool b2 = false;


            Main gameWindow = new Main(b, b2);

            gameWindow.Show();
            this.Close();

        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Linq;
using static Shop.Main;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace Shop
{
   
    public partial class Basket : Window
    {
        public ObservableCollection<ProductViewModel> BasketProducts { get; set; }

        public string Wallete { get; set; }

        public Basket(ObservableCollection<ProductViewModel> products, string bea, string aq, string n)
        {
            InitializeComponent();
            BasketProducts = products;
            DataContext = this;
            CalculateTotalPrice();
            Wallete = bea;
            ba.Text = aq;
            nam.Text = n;
        }

        public void SetBasketProducts(ObservableCollection<ProductViewModel> products)
        {
            BasketProducts = products;

            CalculateTotalPrice();
        }
        private void CalculateTotalPrice()
        {
            double totalPrice = BasketProducts.Sum(product => product.Price * product.Quantity);
            sum.Text = totalPrice.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(Wallete) > int.Parse(sum.Text))
            {
                if (sum.Text == "0")
                {
                    MessageBox.Show("в корзине ничего нет");
                }
                else
                {
                    Wallete = (int.Parse(Wallete) - int.Parse(sum.Text)).ToString();
                    var context = new AppDbContext();
                    var user = context.Users.SingleOrDefault(x => x.Login == nam.Text);

                    user.Balance = user.Balance - int.Parse(sum.Text);
                    context.SaveChanges();
                    MessageBox.Show("оплата успешно проведена");
                    list.ItemsSource = null;

                    Main gameWindow = new Main(int.Parse(Wallete), true, nam.Text);
                    gameWindow.Show();
                    this.Close();
                }        
            }
            else
            {
                MessageBox.Show("вам не хватает средств");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main gameWindow = new Main(int.Parse(Wallete), false, nam.Text);
            gameWindow.Show();
            this.Close();
        }
        private void btn_exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var context = new AppDbContext();
            var user = context.Users.SingleOrDefault(x => x.Login == nam.Text);
            if (popo == null)
            {
                MessageBox.Show("Введите сумму пополнения");
            }
            else
            {
                user.Balance = user.Balance + int.Parse(popo.Text);
                context.SaveChanges();
                Wallete = user.Balance.ToString();

                Main gameWindow = new Main(int.Parse(Wallete), true, nam.Text);
                gameWindow.Show();
                this.Close();


                MessageBox.Show("вы пополнили счёт");
                
            }
        }
        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ProductViewModel product = button.Tag as ProductViewModel;
                if (product != null)
                {
                    product.Quantity++; 
                    CalculateTotalPrice(); 
                }
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ProductViewModel product = button.Tag as ProductViewModel;
                if (product != null && product.Quantity > 1)
                {
                    product.Quantity--; 
                    CalculateTotalPrice(); 
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = null;
            
            sum.Text = "0";
        }
    }
}

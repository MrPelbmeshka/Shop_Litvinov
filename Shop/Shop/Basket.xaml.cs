using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Linq;
using static Shop.Main;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.EntityFrameworkCore;

namespace Shop
{
   
    public partial class Basket : Window
    {
        public ObservableCollection<ProductViewModel> BasketProducts { get; set; }

        public string Wallete { get; set; }

        public Basket(ObservableCollection<ProductViewModel> products, string ba)
        {
            InitializeComponent();
            BasketProducts = products;
            DataContext = this;
            CalculateTotalPrice();
            Wallete = ba;
        }

        public void SetBasketProducts(ObservableCollection<ProductViewModel> products)
        {
            BasketProducts = products;

            // После обновления коллекции вызываем CalculateTotalPrice для пересчета суммы
            CalculateTotalPrice();
        }

        // Метод для вычисления общей суммы цен товаров в корзине
        private void CalculateTotalPrice()
        {
            double totalPrice = BasketProducts.Sum(product => product.Price * product.Quantity);
            sum.Text = totalPrice.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(Wallete) > int.Parse(sum.Text))
            {
                Wallete = (int.Parse(Wallete) - int.Parse(sum.Text)).ToString();
                
                MessageBox.Show("оплата успешно проведена");
                list.ItemsSource = null;

                Main gameWindow = new Main(int.Parse(Wallete), true);
                gameWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("вам не хватает средств");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main gameWindow = new Main(int.Parse(Wallete), false);
            gameWindow.Show();
            this.Close();
        }
        private void btn_exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

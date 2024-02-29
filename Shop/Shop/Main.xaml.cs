using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;



namespace Shop
{
    public class RelayCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public RelayCommand(Action execute) : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();
    }
    


    public partial class Main : Window
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ProductViewModel> products = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> Products
        {
            get { return products; }
            set
            {
                products = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
            }
        }


        public Main(int b, bool tf, string we)
        {
            InitializeComponent();
            DataContext = this;


            if (tf == true)
            {
                list.ItemsSource = null;
            }
            nam.Text = we;
            ba.Text = b.ToString();
        }

        private void BuyButtonClick_1(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\1.webp", 
                Name = "Заготовка ", 
                Price = 100, 
                Quantity = 1 
            });
        }
        private void BuyButtonClick_2(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\2.jpg", 
                Name = "Станок резинок ", 
                Price = 200, 
                Quantity = 1 
            });
        }
        private void BuyButtonClick_3(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\3.webp",
                Name = "Резинки ",
                Price = 300,
                Quantity = 1
            });
        }
        private void BuyButtonClick_4(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\4.jpg",
                Name = "Спицы ",
                Price = 250,
                Quantity = 1
            });
        }
        private void BuyButtonClick_5(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\5.jpg",
                Name = "Ёлка ",
                Price = 157,
                Quantity = 1
            });
        }
        private void BuyButtonClick_6(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\6.jpg",
                Name = "Дерево ",
                Price = 150,
                Quantity = 1
            });
        }
        private void BuyButtonClick_7(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\7.jpg",
                Name = "Нюша ",
                Price = 124,
                Quantity = 1
            });
        }
        private void BuyButtonClick_8(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\8.jpg",
                Name = "Нитки ",
                Price = 121,
                Quantity = 1
            });
        }
        private void BuyButtonClick_9(object sender, RoutedEventArgs e)
        {
            Products.Add(new ProductViewModel
            {
                ImagePath = "Image\\9.jpg",
                Name = "Станок ",
                Price = 1293,
                Quantity = 1
            });
        }

        private void btn_exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public class ProductViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public ICommand IncreaseQuantityCommand { get; private set; }

            public ProductViewModel()
            {
                // Инициализация команды
                IncreaseQuantityCommand = new RelayCommand(IncreaseQuantity);
                DecreaseQuantityCommand = new RelayCommand(DecreaseQuantity, () => Quantity > 0);
            }

            private void IncreaseQuantity()
            {
                Quantity++; // Увеличиваем количество товара
                Price += Price; // Увеличиваем стоимость на цену товара
            }

            public ICommand DecreaseQuantityCommand { get; private set; }


            private void DecreaseQuantity()
            {
                Quantity--; // Уменьшаем количество товара
                Price -= Price; // Уменьшаем стоимость на цену товара
            }



            private string imagePath;
            public string ImagePath
            {
                get { return imagePath; }
                set
                {
                    imagePath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagePath)));

                }

            }

            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }

            private double price;
            public double Price
            {
                get { return price; }
                set
                {
                    price = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
                }
            }

            private int quantity;
            public int Quantity
            {
                get { return quantity; }
                set
                {
                    quantity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
                }
            }

        }
    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string q = ba.Text;
            string w  = q;
            string n = nam.Text;

            Basket basketWindow = new Basket(Products, q,w, n);
            basketWindow.Show();
            this.Close();
        }


    }
}
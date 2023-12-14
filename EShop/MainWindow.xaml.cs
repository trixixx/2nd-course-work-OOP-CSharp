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
using System.Xml.Serialization;

namespace EShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<AuthenticatedCustomer> users = new List<AuthenticatedCustomer>();
        List<Product> products = new List<Product>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginWindow loginWindow = new loginWindow();

            if (loginWindow.ShowDialog() == true)
            {
                AuthenticatedCustomer res = users.Find(x => x.login == loginWindow.Login)!;
                if (res != null)
                {
                    try
                    {
                        res.Login(loginWindow.Password);
                        logout.IsEnabled = true;
                        logout.Visibility = Visibility.Visible;
                        LoginButton.IsEnabled = false;
                        LoginButton.Visibility = Visibility.Collapsed;
                        Signup.IsEnabled = false;
                        Signup.Visibility = Visibility.Collapsed;
                        Username.IsEnabled = true;
                        Username.Visibility = Visibility.Visible;
                        Username.Text = res.Name;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Неправильный пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин");
                }
            }
            else
            {
                MessageBox.Show("Авторизация не пройдена");
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            signupwindow signupwindow = new signupwindow();

            if (signupwindow.ShowDialog() == true)
            {
                AuthenticatedCustomer newCustomer = new AuthenticatedCustomer
                    (
                    signupwindow.Login, signupwindow.Password, signupwindow.Name, signupwindow.Adress
                    );
                users.Add(newCustomer);
                newCustomer.Login(signupwindow.Password);
                logout.IsEnabled = true;
                logout.Visibility = Visibility.Visible;
                LoginButton.IsEnabled = false;
                LoginButton.Visibility = Visibility.Collapsed;
                Signup.IsEnabled = false;
                Signup.Visibility = Visibility.Collapsed;
                Username.IsEnabled = true;
                Username.Text = newCustomer.Name;
                Username.Visibility = Visibility.Visible;
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            AuthenticatedCustomer CurrentCustomer = users.Find(x => x.IsActive == true)!;
            if (CurrentCustomer is not null)
            {
                CurrentCustomer.Logout();
                logout.IsEnabled = false;
                logout.Visibility= Visibility.Collapsed;
                LoginButton.IsEnabled = true;
                LoginButton.Visibility = Visibility.Visible;
                Signup.IsEnabled = true;
                Signup.Visibility = Visibility.Visible;
                Username.IsEnabled = false;
                Username.Visibility= Visibility.Collapsed;
            }
        }
    }

    abstract class BaseUser
    {
        protected int id;
        protected static int counter = 0;
    }

    abstract class Customer : BaseUser
    {
        public Cart cart { get; }
        public Customer() 
        { 
            id = counter++;
            cart = new Cart();
        }
    }
    
    class AnonymousCustomer : Customer
    {
    }

    class AuthenticatedCustomer : Customer
    {
        public string login { get; private set; }
        private string password;
        public string Name, Adress;

        public bool IsActive { get; private set; }

        public AuthenticatedCustomer(string login, string password, string name, string adress) : base()
        {
            this.login = login; this.password = password;
            this.Adress = adress; this.Name = name;
        }

        public void Login(string password)
        {
            if (password == this.password)
            {
                this.IsActive = true;
                return;
            }
            throw new ArgumentException();
        }
        public void Logout()
        { this.IsActive = false; }
    }

    class Product
    {
        public int id { get; }
        private static int counter = 0;
        public string name { get; }
        public string? description { get; set; }
        public double price { get; set; }
        public Product(string _name)
        {
            id = counter++;
            name = _name;
        }
    }

    class Cart
    {
        public int id;
        public List<Product> products = new List<Product>();
        public void Clear() { products.Clear(); }
        public void Add(Product product) { products.Add(product); }
        public void Remove(Product product) { products.Remove(product);}
    }

    class Subscription : Cart
    {
        public int period;
        public Subscription(Cart c)
        {
            this.id = c.id;
            this.products = c.products;
        }
    }

    class Order
    {
        public string CustomerName;
        public string Adress;
        public double Price;
        public bool IsPayed { get; private set; }

        public Order(Cart c, string customername, string adress)
        {
            this.CustomerName = customername;
            this.Adress = adress;
            this.Price = 0;
            foreach (var pr in c.products)
            {
                this.Price += pr.price;
            }
        }
    }
}
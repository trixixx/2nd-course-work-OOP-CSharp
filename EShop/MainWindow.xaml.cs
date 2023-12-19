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
        List<AuthenticatedCustomer> customers = new List<AuthenticatedCustomer>();
        Customer current_customer = new AnonymousCustomer();
        Product cola = new Product("Cola");
        Product bread = new Product("Bread");
        Product milk = new Product("Milk");
        Product trashbags = new Product("Trash bags");
        public MainWindow()
        {
            InitializeComponent();
            
            cola.price = 150;
            cola.description = "Just cola, zero sugar -_-";

            bread.price = 60;
            bread.description = "Bread, tasty";

            milk.price = 90;
            milk.description = "cheap milk, outdated maybe";

            trashbags.price = 125;
            trashbags.description = "something useful finally";


        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginWindow loginWindow = new loginWindow();

            if (loginWindow.ShowDialog() == true)
            {
                AuthenticatedCustomer res = customers.Find(x => x.login == loginWindow.Login)!;
                if (res != null)
                {
                    try
                    {
                        res.Login(loginWindow.Password);
                        current_customer = res;
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
                customers.Add(newCustomer);
                newCustomer.Login(signupwindow.Password);
                current_customer = newCustomer;
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
            AuthenticatedCustomer CurrentCustomer = customers.Find(x => x.IsActive == true)!;
            if (CurrentCustomer is not null)
            {
                CurrentCustomer.Logout();
                current_customer = new AnonymousCustomer();
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

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow(current_customer);
            cartWindow.Show();
        }

        private void add1_Click(object sender, RoutedEventArgs e)
        {
            current_customer.cart.Add(cola);
        }

        private void add2_Click(object sender, RoutedEventArgs e)
        {
            current_customer.cart.Add(bread);
        }

        private void add3_Click(object sender, RoutedEventArgs e)
        {
            current_customer.cart.Add(milk);
        }

        private void add4_Click(object sender, RoutedEventArgs e)
        {
            current_customer.cart.Add(trashbags);
        }
    }
}
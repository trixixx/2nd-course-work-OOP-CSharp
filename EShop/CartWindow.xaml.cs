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

namespace EShop
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        Customer _customer;
        public CartWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            products.ItemsSource = customer.cart.products;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow;
            if (_customer is AuthenticatedCustomer)
            {
                orderWindow = new OrderWindow((AuthenticatedCustomer)_customer);
            }
            else
            {
                orderWindow = new OrderWindow((AnonymousCustomer)_customer);
            }
            orderWindow.Show();
        }

        private void subButton_Click(object sender, RoutedEventArgs e)
        {
            if (_customer is AuthenticatedCustomer)
            {
                Subscription subscription = new Subscription(_customer.cart);
                subscription.period = 1;
                _customer.cart.products.Clear();
            }
            else
            {
                MessageBox.Show("Войдите чтобы создать подписку на продукты");
            }
        }
    }
}

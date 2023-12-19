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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        Customer _customer;
        Order? order;
        public OrderWindow(AuthenticatedCustomer customer)
        {
            InitializeComponent();
            order = new Order(customer.cart, customer.Name, customer.Adress);
            Adress.Text = order.Adress;
            Adress.IsEnabled = false;
            FullName.Text = order.CustomerName;
            FullName.IsEnabled = false;
            Total.Text = order.Price.ToString();
            _customer = customer;
        }
        public OrderWindow(AnonymousCustomer customer)
        {
            InitializeComponent();
            double tt = 0;
            foreach(var pr in customer.cart.products)
            {
                tt += pr.price;
            }
            Total.Text = tt.ToString();
            _customer = customer;
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if(order != null) { order.IsPayed = true; }
            else
            {
                order = new Order(_customer.cart, FullName.Text, Adress.Text);
                order.IsPayed = true;
            }
            _customer.cart.products.Clear();
        }
    }
}

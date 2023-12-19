using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    public abstract class Customer : BaseUser
    {
        public Cart cart { get; }
        public Customer()
        {
            id = counter++;
            cart = new Cart();
        }
    }

    public class AnonymousCustomer : Customer
    {
    }

    public class AuthenticatedCustomer : Customer
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
}

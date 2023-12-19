using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    public class Order
    {
        public string CustomerName;
        public string Adress;
        public double Price;
        public bool IsPayed { get; set; }

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

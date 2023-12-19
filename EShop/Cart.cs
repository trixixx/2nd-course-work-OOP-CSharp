using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    public class Cart
    {
        public int id;
        public List<Product> products = new List<Product>();
        public void Clear() { products.Clear(); }
        public void Add(Product product) { products.Add(product); }
        public void Remove(Product product) { products.Remove(product); }
    }

    public class Subscription : Cart
    {
        public int period;
        public Subscription(Cart c)
        {
            this.id = c.id;
            this.products = c.products;
        }
    }
}
